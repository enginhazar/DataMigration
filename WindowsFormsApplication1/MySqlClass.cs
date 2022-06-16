using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication1
{
    public class MySqlClass
    {
        public MySqlConnection SourceConnection;
        MySqlCommand SourceCommand;
        string getschema = "show table STATUS from ";

        public void Connect(string connectionString)
        {
            SourceConnection = new MySqlConnection(connectionString);
            SourceCommand = new MySqlCommand();
            SourceCommand.Connection = SourceConnection;
            SourceConnection.Open();
        }


        public DataTable GetTable(string tableFilter)
        {
            string sql = getschema + SourceConnection.Database.ToString();
            if (tableFilter != "")
            {
                tableFilter += tableFilter.IndexOf("%") > 0 ? "" : "%";
                sql += string.Format(" like '{0}'", tableFilter);
            }
            SourceCommand.CommandText = sql;
            using (MySqlDataAdapter adp = new MySqlDataAdapter(SourceCommand))
            {
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;

            }

        }

        internal string CreateSQLSchema(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  * from {0} limit 1", tableName);

            SourceCommand.CommandText = sql;
            MySqlDataReader rd = SourceCommand.ExecuteReader();
            DataTable tblSchema = rd.GetSchemaTable();

            string dropSQL = string.Format(@"if exists(select * from sys.tables where name='{0}')
                                drop table {0}
                                ", tableName);
            string createSql = string.Format(@"Create Table dbo.[{0}] ", tableName);
            string columnsSql = "";
            for (int i = 0; i < tblSchema.Rows.Count; i++)
            {
                string columnSql = GetSqlColumnSchema(tblSchema.Rows[i]);
                columnsSql += "," + columnSql + "\r\n";
            }
            rd.Close();

            return dropSQL + createSql + "(" + columnsSql.Substring(1, columnsSql.Length - 1) + ")";
        }

        private string GetSqlColumnSchema(DataRow dataRow)
        {
            string column = dataRow["ColumnName"].ToString();
            if (column.ToUpper() == "TOP" || column.ToUpper() == "END" || column.ToUpper() == "PLAN")
                column = "[" + column + "]";
            MySqlDbType dataType = (MySqlDbType)dataRow["ProviderType"];
            string columnType = GetSqlType(dataType);
            //if (dataType == MySqlDbType.String)
            //{
            //    if (Convert.ToInt32(dataRow["ColumnSize"]) > 0)
            //    {
            //        if (Convert.ToInt32(dataRow["ColumnSize"]) > 10)
            //            columnType += string.Format("({0})", dataRow["ColumnSize"].ToString());
            //        else
            //            columnType += string.Format("({0})", 50);
            //    }
            //    else
            //        columnType = "varchar(max)";
            //}

            return column + " " + columnType + " null";
        }

        private string GetSqlType(MySqlDbType dataType)
        {
            switch (dataType)
            {
                case MySqlDbType.Binary:
                    return "varbinary(max)";

                case MySqlDbType.Bit:
                    return "bit";

                case MySqlDbType.Blob:
                    return "varbinary(max)";

                case MySqlDbType.Byte:
                    return "varbinary(max)";

                case MySqlDbType.Date:
                    // return "varchar(20)";
                    return "datetime2";

                case MySqlDbType.DateTime:
                    //return "varchar(20)";
                    return "datetime2";


                case MySqlDbType.Decimal:
                    return "decimal";

                case MySqlDbType.Double:
                    return "money";

                case MySqlDbType.Enum:
                    return "int";

                case MySqlDbType.Float:
                    return "float";

                case MySqlDbType.Geometry:
                    return "varbinary(max)";

                case MySqlDbType.Guid:
                    return "varbinary(max)";

                case MySqlDbType.Int16:
                    return "int";

                case MySqlDbType.Int24:
                    return "int";

                case MySqlDbType.Int32:
                    return "int";

                case MySqlDbType.Int64:
                    return "int";

                case MySqlDbType.LongBlob:
                    return "varbinary(max)";

                case MySqlDbType.LongText:
                    return "varchar(max)";

                case MySqlDbType.MediumBlob:
                    return "varbinary(max)";
                case MySqlDbType.MediumText:
                    return "varchar(max)";

                case MySqlDbType.NewDecimal:
                    return "money";
                case MySqlDbType.Newdate:
                    return "datetime";
                case MySqlDbType.Set:
                    return "varchar(max)";
                case MySqlDbType.String:
                    return "varchar(max)";

                case MySqlDbType.Text:
                    return "varchar(max)";

                case MySqlDbType.Time:
                    return "time";

                case MySqlDbType.Timestamp:
                    return "timestamp";

                case MySqlDbType.TinyBlob:
                    return "varbinary(max)";
                case MySqlDbType.TinyText:
                    return "varchar(max)";
                case MySqlDbType.UByte:
                    return "varbinary(max)";
                case MySqlDbType.UInt16:
                    return "int";
                case MySqlDbType.UInt24:
                    return "int";
                case MySqlDbType.UInt32:
                    return "int";
                case MySqlDbType.UInt64:
                    return "int";
                case MySqlDbType.VarBinary:
                    return "varbinary)(max)";
                case MySqlDbType.VarChar:
                    return "varchar(max)";
                case MySqlDbType.VarString:
                    return "varchar(max)";
                case MySqlDbType.Year:
                    return "int";
                default:
                    return "varchar(max)";
            }
        }



        internal IDataReader GetDataReader(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  * from {0} ", tableName);

            SourceCommand.CommandText = sql;
            MySqlDataReader rd = SourceCommand.ExecuteReader();
            return rd;
        }

        internal DataTable GetDataTable(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  * from {0}", tableName);

            SourceCommand.CommandText = sql;
            using (MySqlDataAdapter adp = new MySqlDataAdapter(SourceCommand))
            {
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;

            }

        }

        internal string GetInsertSQL(string tableName)
        {
            IDataReader rd = this.GetDataReader("gelirgode");
            DataTable rdSchema = rd.GetSchemaTable();
            string insertSQL = "";
            string.Format("Insert Into {0} values ", "gelirgode");
            int i = 0;
            while (rd.Read())
            {
                string insertcolumn = "";
                //rd.GetValue();//
                for (int x = 0; x < rd.FieldCount; x++)
                {
                    //IDataRecord rc = rd.GetData(x);
                    object v = null;
                    if ((rd.GetDataTypeName(x) == "DATE" || rd.GetDataTypeName(x) == "DATETIME") && !rd.IsDBNull(x))
                    {
                        v = rd.GetValue(x);
                        DateTime t = new DateTime();
                        DateTime.TryParse(v.ToString(), out t);
                        if (t.Year < 1800)
                            v = null;
                    }
                    else
                    {
                        v = rd.GetValue(x);
                    }
                    //object v = GetReaderValue(rd);
                    if (v != null && v.ToString() != "")
                    {
                        switch (rd.GetDataTypeName(x))
                        {
                            case "DATE":
                                insertcolumn += " ,'" + v.ToString() + "'";
                                break;
                            case "DATETIME":
                                insertcolumn += " ,'" + v.ToString() + "'";
                                break;
                            case "VARCHAR":
                                insertcolumn += " ,'" + v.ToString() + "'";
                                break;
                            case "CHAR":
                                insertcolumn += " ,'" + v.ToString() + "'";
                                break;
                            case "DOUBLE":
                                insertcolumn += " ," + v.ToString().Replace(",", ".") + "";
                                break;
                            case "FLOAT":
                                insertcolumn += " ," + v.ToString().Replace(",", ".") + "";
                                break;
                            case "DECIMAL":
                                insertcolumn += " ," + v.ToString().Replace(",", ".") + "";
                                break;
                            default:
                                insertcolumn += " ," + v.ToString();
                                break;
                        }
                        ;
                    }
                    else
                        insertcolumn += " ," + "NULL";
                }
                i++;
                insertSQL += string.Format("Truncate Table{0} \r\n Insert Into {0}  values ({1}) \r\n", "gelirode", insertcolumn.Substring(2, insertcolumn.Length - 2));

            }
            rd.Close();
            return insertSQL;
        }
    }
}
