using DbConverter.Provider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace DbConverter
{
    public partial class DbConverterFrm : Form
    {
        BaseDbProvider SourceProvider;
        BaseDbProvider TargetProvider;
        string LOGFILE = $@"{System.Environment.CurrentDirectory}\\Log_{DateTime.Now.ToString("yyyyMMdd")}.txt";
        public StreamWriter fileLog = null;

        public DbConverterFrm()
        {
            InitializeComponent();
            foreach (var item in Enum.GetNames(typeof(DATABASETYPE)))
            {
                sourceDatabaseTypeCombo.Items.Add(item);


                targetDatabaseTypeCombo.Items.Add(item);

                //targetDatabaseTypeCombo.SelectedItem = DATABASETYPE.POSTGRES;
            }
            sourceDatabaseTypeCombo.SelectedIndex = 0;
            targetDatabaseTypeCombo.SelectedIndex = 1;
            InitControls();


        }

        private void InitControls()
        {
            toolStripLabel1.Text="";
        }

        private void sourceConnectBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(sourceConnectionStringTxtBox.Text))
            {
                MessageBox.Show("Source Connection String Empty");
                return;
            }

            SourceProvider = CreateProvider((DATABASETYPE)sourceDatabaseTypeCombo.SelectedIndex);
            SourceProvider.Connect(sourceConnectionStringTxtBox.Text);
            sourceConnectionStringTxtBoxState("Connected");
            EnabledConvertButton();
        }

        private void EnabledConvertButton()
        {
            convertBtn.Enabled=SourceProvider!=null &&SourceProvider.Connection !=null && TargetProvider!=null && TargetProvider.Connection!=null  && sourceTableGridView.DataSource!=null;
        }

        private void sourceConnectionStringTxtBoxState(string status)
        {
            sourceConnectBtn.Text = status;
            ToolStripLabelChange();
        }

        private BaseDbProvider CreateProvider(DATABASETYPE databaseType)
        {
            BaseDbProvider ret = null;
            switch (databaseType)
            {
                case DATABASETYPE.SQL:
                    ret = new SqlProvider(this, databaseType);
                    break;
                case DATABASETYPE.POSTGRES:
                    ret = new PostgresProvider(this, databaseType);
                    break;
                case DATABASETYPE.MYSQL:
                    break;
                case DATABASETYPE.DB2:
                    break;
                default:
                    break;
            }
            return ret;

        }

        private void GetTableButton_Click(object sender, EventArgs e)
        {
            List<Table> tables = SourceProvider.GetTables();
            sourceTableGridView.DataSource = tables;
            EnabledConvertButton();
            //sourceTableGridView.
        }

        private void targetConnectBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(targetConnectionStringTxtBox.Text))
            {
                MessageBox.Show("Target Connection String Empty");
                return;
            }

            TargetProvider = CreateProvider((DATABASETYPE)targetDatabaseTypeCombo.SelectedIndex);
            TargetProvider.Connect(targetConnectionStringTxtBox.Text);
            targetConnectionStringTxtBoxState("Connected");
            EnabledConvertButton();
        }

        private void targetConnectionStringTxtBoxState(string status)
        {
            targetConnectBtn.Text = status;
            ToolStripLabelChange();
        }

        private void ToolStripLabelChange()
        {
            string sourceDbString= SourceProvider!=null?$"Source Database {SourceProvider.Connection.DataSource} is Connected":"";
            string targetDbString = TargetProvider!=null?$"Target Database {TargetProvider.Connection.DataSource} is Connected":"";
            toolStripLabel1.Text=$"{sourceDbString}-{targetDbString}";
        }

        private void convertBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (TargetProvider.Connection.State == ConnectionState.Closed)
                    TargetProvider.Connect(targetConnectionStringTxtBox.Text);

                Convert();
                MessageBox.Show("Convert is Completed");

            }
            catch (DbException dbexp)
            {
                MessageBox.Show(dbexp.ToString(), "DbException");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString(), "Exception");

            }
            finally
            {
                SourceProvider.Connection.Close();
                TargetProvider.Connection.Close();
                fileLog.Close();
            }

        }

        private void Convert()
        {


            CreateteLogFile();

            string dbName = targetDatabaseTxt.Text.ToLower();

            if (string.IsNullOrWhiteSpace(dbName))
            {
                MessageBox.Show("Target Database String is Empty");
                return;
            }
            if (SourceProvider.Connection.State == ConnectionState.Closed)
                SourceProvider.Connection.Open();

            if (TargetProvider.Connection.State == ConnectionState.Closed)
                TargetProvider.Connection.Open();


            bool isExistsDb = TargetProvider.ExistsDB(dbName);
            if (isExistsDb && RecreateDatabase.Checked)
            {
                TargetProvider.SetTargetDatabaseConnection("postgres");

                TargetProvider.DatabaseAllConnectionKill(dbName);


                TargetProvider.DeleteDB(dbName);

                TargetProvider.CreateDB(dbName);


            }

            if (!isExistsDb)
            {
                TargetProvider.CreateDB(dbName);
            }

            if (sourceTableGridView.DataSource == null)
                return;

            TargetProvider.SetTargetDatabaseConnection(dbName);


            Dictionary<string, string> schemaList = new Dictionary<string, string>();
            try
            {
                CreateTable(schemaList);
                
                DataImport();

                CreateIndex();
            }
            catch (Exception ex)
            {

                throw ex;
            }






        }

        private void CreateIndex()
        {
            if (TablesCreateIndex.Checked) /// sadece data convert et
            {

                foreach (Table table in (List<Table>)sourceTableGridView.DataSource)
                {
                    // index kolonları bulunuyor
                    Table tablex = table;
                    SourceProvider.GetIndexColumns(ref tablex);

                    TargetProvider.CreateTableIndex(tablex);
                }
            }
        }

        private void DataImport()
        {
            if (DataConvertCheck.Checked) /// sadece data convert et
            {
                toolStripLabel1.Text="Data is Converting...";
                toolStrip1.Refresh();
                ConvertProgressBar.Value=0;
                ConvertProgressBar.Maximum=((List<Table>)sourceTableGridView.DataSource).Count;
                try
                {
                    foreach (Table table in (List<Table>)sourceTableGridView.DataSource)
                    {
                        if (truncateTableChecked.Checked)
                            TargetProvider.TruncateTable(table);

                        IDataReader reader = SourceProvider.GetSourceTableDataReader(table);
                        TargetProvider.BulkInsertData(table, reader);

                        TargetProvider.SetIdentityValue(table);
                        ConvertProgressBar.Value++;
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
                toolStripLabel1.Text="Data is Converted...";
                toolStrip1.Refresh();
            }
        }

        private void CreateTable(Dictionary<string, string> schemaList)
        {
            toolStripLabel1.Text="Creating Tables...";
            toolStrip1.Refresh();
            ConvertProgressBar.Value=0;
            ConvertProgressBar.Maximum=((List<Table>)sourceTableGridView.DataSource).Count;
            foreach (Table item in (List<Table>)sourceTableGridView.DataSource)
            {
                if (!schemaList.ContainsKey(item.SchemaName))
                {
                    bool isExistingSchema = TargetProvider.IsExistingSchema(item);
                    if (!isExistingSchema)
                    {
                        TargetProvider.CreateSchema(item, targeDatabaseRoleTxt.Text);
                        schemaList.Add(item.SchemaName, item.SchemaName);

                    }
                }

                bool isExistsTable = TargetProvider.IsExistsTable(item);
                if (isExistsTable && recreateTableCheck.Checked)
                {
                    TargetProvider.DeleteTable(item);
                    isExistsTable = false;
                }

                if (!isExistsTable)
                    TargetProvider.CreateTable(item);
                ConvertProgressBar.Value++;
            }
            toolStripLabel1.Text="Tables Created.";
            toolStrip1.Refresh();
        }

        public void LogYaz(string log)
        {
            string logWrite = $@"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}  {log}";
            richTextBox1.Text = "\r\n" + log + richTextBox1.Text;
            richTextBox1.Refresh();
            FileWriteLog(log);

        }

        private void FileWriteLog(string log)
        {
            fileLog.WriteLine(log);
        }

        private void CreateteLogFile()
        {
            fileLog = File.CreateText(LOGFILE);
        }

        private void sourceConnectionStringTxtBox_TextChanged(object sender, EventArgs e)
        {

        }

       
    }



    public enum DATABASETYPE
    {
        SQL = 0,
        POSTGRES = 1,
        MYSQL = 2,
        ORACLE = 3,
        DB2 = 4
    }

}
