using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class SqlClass
    {
       
        public SqlConnection sqlConnection;
        SqlCommand sqlCommand;
        Form1 form = null;
        public StreamWriter fileLog=null;
        public void Connect(string connectionString,Form1 formP)
        {
            sqlConnection = new SqlConnection(connectionString);
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = sqlConnection;
            sqlConnection.Open();
            form=formP;

            fileLog = File.CreateText(System.Environment.CurrentDirectory+"\\Log1.txt");
            
        }

        public void LogYaz(string log)
        {
            fileLog.WriteLine(log);
        }

        internal void CreateSQL(string getSchemaSql)
        {
            sqlCommand.CommandText = getSchemaSql;
            sqlCommand.ExecuteScalar();
        }

        public DataTable GetData(string sql)
        {
            sqlCommand.CommandText = sql;
            using (SqlDataAdapter adp = new SqlDataAdapter(sqlCommand))
            {
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;

            }
        }

        internal void ImportData(System.Data.IDataReader reader, string tableName,int rowCount)
        {
            
            sqlCommand.CommandText = "truncate table " + tableName;
            sqlCommand.ExecuteScalar();
            using (SqlBulkCopy sqb = new SqlBulkCopy(sqlConnection))
            {
                sqb.DestinationTableName = tableName;
                sqb.BulkCopyTimeout = 0;
                sqb.BatchSize = 0;
                    
                try
                {
                    sqb.NotifyAfter = 10000;
                    sqb.SqlRowsCopied += sqb_SqlRowsCopied;
                    sqb.WriteToServer(reader);
                    sqb.Close();
                }
                //catch (MySql.Data.Types.MySqlConversionException ex)
                //{

                //}

                catch (SqlException sqlex)
                {

                }
                catch (Exception ex)
                {
                    LogYaz(string.Format("{0} hata", reader[0].ToString()));
                    form.richTextBoxLog.Text += "\r\n" + reader[0].ToString();
                    throw;
                }

            }


        }

        void sqb_SqlRowsCopied(object sender, SqlRowsCopiedEventArgs e)
        {
            
            form.richTextBoxLog.Text = string.Format(" Tablo {0}  -{1} Aktarıldı \r\n",((SqlBulkCopy)sender).DestinationTableName, e.RowsCopied) + form.richTextBoxLog.Text;
                form.richTextBoxLog.Refresh();
                LogYaz(string.Format("{0} Aktarıldı", e.RowsCopied));
            
                
        }

        internal void ImportData(DataTable tbl, string tableName)
        {
            sqlCommand.CommandText="truncate table "+tableName;
            sqlCommand.ExecuteNonQuery();
            using (SqlBulkCopy sqb = new SqlBulkCopy(sqlConnection))
            {
                sqb.DestinationTableName = tableName;
                sqb.BulkCopyTimeout = 0;
                try
                {
                    sqb.NotifyAfter = 10000;
                    sqb.SqlRowsCopied += sqb_SqlRowsCopied;
                    sqb.WriteToServer(tbl);
                }
                //catch (MySql.Data.Types.MySqlConversionException ex)
                //{

                //}

                catch (SqlException sqlex)
                {
                    string s = string.Format(@"Table={0}
                    Kayıt Sıra={1}
                    SQL Exception
                    {2}
                    ----------------------------"
                        , tableName, sqb.NotifyAfter, sqlex.Message);
                    form.richTextBoxLog.Text += "\r\n" + s;
                    LogYaz(s);
                }
                catch (Exception e1)
                {
                    string s = string.Format(@" Table={0}
                    Kayıt Sıra={1}
                       Exception
                    {2}
                    ----------------------------"
                        , tableName, sqb.NotifyAfter, e1.InnerException.ToString());
                    form.richTextBoxLog.Text +="\r\n"+ s;
                    LogYaz(s);
                    
                }

            }
        }

        internal void ImportDataSQL(string tblInsert)
        {
            try
            {

            }
        
            catch (SqlException sqlex)
                {
                    string s = string.Format(@"
                  
                    SQL Exception
                    {0}
                    ----------------------------",
                    sqlex.Message);
                    form.richTextBoxLog.Text += "\r\n" + s;
                    LogYaz(s);
                }
                catch (Exception e1)
                {
                    string s = string.Format(@"
                  
                     Exception
                    {0}
                    ----------------------------",
                    e1.Message);
                    form.richTextBoxLog.Text += "\r\n" + s;
                    form.richTextBoxLog.Text +="\r\n"+ s;
                    LogYaz(s);
                    
                }
            sqlCommand.CommandText = tblInsert;
            sqlCommand.ExecuteScalar();
        }
    }
}
