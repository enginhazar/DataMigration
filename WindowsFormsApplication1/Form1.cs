
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
      //  DB2Class kaynakClass;
        SqlClass hedefClass;
        public Form1()
        {
            InitializeComponent();
        }

        private void comboProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboProvider.SelectedIndex)
            {
                case 0:
                    connectionString.Text = "server=192.168.11.252:31031;Database=SISTEM;UID=cemil;PWD=Xyz1357";
                    //connectionString.Text = "Dsn=test;uid=root;Option=3;";
                    hedefConnectionString.Text = "Server=192.168.11.19;Database=DEFNETEST;User Id=sa;Password=1357;";
                    break;
                default:
                    break;
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            //kaynakClass = new DB2Class();
            //kaynakClass.Connect(this.connectionString.Text);
            //groupBox1.Text = "Kaynak Data -" + kaynakClass.SourceConnection.State.ToString();
            //button1.Enabled = true;
            //textTableNameFilter.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BindList();
        }

        private void BindList()
        {
            //DataTable table = kaynakClass.GetTable(textTableNameFilter.Text);
            //tableListBox.Items.Clear();
            //for (int i = 0; i < table.Rows.Count; i++)
            //{
            //    tableListBox.Items.Add(string.Format("{0}.{1}", table.Rows[i]["CREATOR"].ToString().TrimEnd(), table.Rows[i]["NAME"].ToString()));


            //}


        }

        private void buttonHedef_Click(object sender, EventArgs e)
        {
            hedefClass = new SqlClass();
            hedefClass.Connect(hedefConnectionString.Text, this);
            groupBox2.Text = "Hedef Data -" + hedefClass.sqlConnection.State.ToString();
            buttonHedefTabloOlustur.Enabled = true;
            buttonKayitAktar.Enabled = true;
        }

        private void buttonHedefTabloOlustur_Click(object sender, EventArgs e)
        {
            HedefTabloOlustur();


        }

        private void HedefTabloOlustur()
        {
            progressBar1.Maximum = tableListBox.Items.Count;
            progressBar1.Value = 0;

            for (int i = 0; i < tableListBox.Items.Count; i++)
            {
                string tableName = tableListBox.Items[i].ToString();
                string getSchemaSql = kaynakClass.CreateSQLSchema(tableName);
                hedefClass.CreateSQL(getSchemaSql);
                progressBar1.Value += 1;

            }
            MessageBox.Show("Dosya Oluşturma Tamamlandı");
        }

        private void buttonKayitAktar_Click(object sender, EventArgs e)
        {
            KayitAktar();
        }

        private void KayitAktar()
        {
            progressBar1.Maximum = tableListBox.Items.Count;
            progressBar1.Value = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < tableListBox.Items.Count; i++)
            {
                string tableName = tableListBox.Items[i].ToString().Substring(0, tableListBox.Items[i].ToString().IndexOf("-"));

                richTextBoxLog.Text = string.Format("\r\n Tablo = {0} Okunuyor...", tableName) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Tablo = {0} Okunuyor...", tableName));
                this.richTextBoxLog.Refresh();                
                DataTable tbl = kaynakClass.GetDataTable(tableName);                
                int rowsCount = tbl.Rows.Count;

                richTextBoxLog.Text = string.Format("\r\n Tablo={0} Rows={1}", tableName, rowsCount) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Tablo={0} Rows={1} ", tableName, rowsCount));
                this.richTextBoxLog.Refresh();

                hedefClass.ImportData(tbl, tableName);
                
                richTextBoxLog.Text = string.Format(" \r\nTable={0}  {1} Kayıt Aktarıldı", tableName, rowsCount) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Table={0}  {1} Kayıt Aktarıldı", tableName, rowsCount));
                progressBar1.Value += 1;
                this.richTextBoxLog.Refresh();
                progressBar1.Refresh();
                //reader.Close();

            }
            stopwatch.Stop();
            MessageBox.Show("Tablo Aktarım Tamamlandı " + stopwatch.Elapsed.Minutes.ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hedefClass.fileLog != null)
                hedefClass.fileLog.Flush();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            KayitAktarFile();

        }

        private void KayitAktarFile()
        {
            //progressBar1.Maximum = tableListBox.Items.Count;
            //progressBar1.Value = 0;
            //for (int i = 0; i < tableListBox.Items.Count; i++)
            //{
            //    string tableName = tableListBox.Items[i].ToString().Substring(0, tableListBox.Items[i].ToString().IndexOf("-"));

            //    //Convert.ToInt32((tableListBox.Items[i].ToString().Substring(tableListBox.Items[i].ToString().IndexOf("-")+1, tableListBox.Items[i].ToString().Length - tableListBox.Items[i].ToString().IndexOf("-"))).Trim());

            //    //IDataReader reader = kaynakClass.GetDataReader(tableName);
            //    richTextBoxLog.Text += string.Format("\r\n Tablo = {0} Okunuyor...", tableName);
            //    hedefClass.LogYaz(string.Format(" Tablo = {0} Okunuyor...", tableName));
            //    richTextBoxLog.Refresh();

            //    string tblInsert = kaynakClass.GetDataTable(tableName);

            //    richTextBoxLog.Text += string.Format("\r\n Tablo = {0} Yazılıyor...", tableName);
            //    hedefClass.LogYaz(string.Format(" Tablo = {0} Okunuyor...", tableName));


            //    hedefClass.ImportDataSQL(tblInsert);
            //    richTextBoxLog.Text += string.Format("\r\n Tablo = {0} Yazıldı...", tableName);
            //    hedefClass.LogYaz(string.Format(" Tablo = {0} Yazıldı...", tableName));
            //    progressBar1.Value += 1;
            //    richTextBoxLog.Refresh();
            //    //reader.Close();

            //}
            //MessageBox.Show("Tablo Aktarım Tamamlandı");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = tableListBox.Items.Count;
            progressBar1.Value = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < tableListBox.Items.Count; i++)
            {
                string tableName = tableListBox.Items[i].ToString().Substring(0, tableListBox.Items[i].ToString().IndexOf("-"));

                //Convert.ToInt32((tableListBox.Items[i].ToString().Substring(tableListBox.Items[i].ToString().IndexOf("-")+1, tableListBox.Items[i].ToString().Length - tableListBox.Items[i].ToString().IndexOf("-"))).Trim());

                //IDataReader reader = kaynakClass.GetDataReader(tableName);
                richTextBoxLog.Text = string.Format("\r\n Tablo = {0} Okunuyor...", tableName) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Tablo = {0} Okunuyor...", tableName));
                this.richTextBoxLog.Refresh();

                DataTable tbl = kaynakClass.GetDataTable(tableName);
                int rowsCount = tbl.Rows.Count;
                
                if (rowsCount == 0)
                    continue;

                DataTable hedefRowCountDT = hedefClass.GetData("select count(*) from " + tableName);
                if (Convert.ToInt32(hedefRowCountDT.Rows[0][0]) > 0)
                    continue;

                string getSchemaSql = kaynakClass.CreateSQLSchema(tableName);
                hedefClass.CreateSQL(getSchemaSql);
                


                richTextBoxLog.Text = string.Format("\r\n Tablo={0} Rows={1}", tableName, rowsCount) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Tablo={0} Rows={1} ", tableName, rowsCount));
                this.richTextBoxLog.Refresh();
                hedefClass.ImportData(tbl, tableName);
                richTextBoxLog.Text = string.Format(" \r\nTable={0}  {1} Kayıt Aktarıldı", tableName, rowsCount) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Table={0}  {1} Kayıt Aktarıldı", tableName, rowsCount));
                progressBar1.Value += 1;
                this.richTextBoxLog.Refresh();
                progressBar1.Refresh();
                //reader.Close();

            }
            stopwatch.Stop();
            MessageBox.Show("Tablo Aktarım Tamamlandı " + stopwatch.Elapsed.Minutes.ToString());
        }

        /// <summary>
        /// datareader click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            progressBar1.Maximum = tableListBox.Items.Count;
            progressBar1.Value = 0;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < tableListBox.Items.Count; i++)
            {
                string tableName = tableListBox.Items[i].ToString();

                richTextBoxLog.Text = string.Format("\r\n Tablo = {0} Okunuyor...", tableName) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Tablo = {0} Okunuyor...", tableName));
                this.richTextBoxLog.Refresh();
                int rowsCount = kaynakClass.GetRowCount(tableName);

                IDataReader reader = kaynakClass.GetDataReader(tableName); 
                

                richTextBoxLog.Text = string.Format("\r\n Tablo={0} Rows={1}", tableName, rowsCount) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Tablo={0} Rows={1} ", tableName, rowsCount));
                this.richTextBoxLog.Refresh();

                hedefClass.ImportData(reader, tableName,rowsCount);

                richTextBoxLog.Text = string.Format(" \r\nTable={0}  {1} Kayıt Aktarıldı", tableName, rowsCount) + richTextBoxLog.Text;
                hedefClass.LogYaz(string.Format(" Table={0}  {1} Kayıt Aktarıldı", tableName, rowsCount));
                progressBar1.Value += 1;
                this.richTextBoxLog.Refresh();
                progressBar1.Refresh();
                reader.Close();

            }
            stopwatch.Stop();
            MessageBox.Show("Tablo Aktarım Tamamlandı " + stopwatch.Elapsed.Minutes.ToString());


           
         

        }

      
       
       
    }
}
