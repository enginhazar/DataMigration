using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using IBM.Data.DB2;

namespace WindowsFormsApplication1
{
    public class DB2Class
    {
        public DB2Connection SourceConnection;
        DB2Command SourceCommand;
        string getschema = @"select * from sysibm.systables
where type='T'
and tbspace like 'USER%'
{0}
ORDER BY CREATOR,NAME";

        public void Connect(string connectionString)
        {
            SourceConnection = new DB2Connection(connectionString);
            SourceCommand = new DB2Command();
            SourceCommand.Connection = SourceConnection;
            SourceCommand.CommandTimeout = 0;
            SourceConnection.Open();
        }


        public DataTable GetTable(string tableFilter)
        {
            string sql = getschema;
            if (tableFilter != "")
            {
                
                sql = string.Format(getschema, string.Format(" AND (CREATOR like '%{0}% ' or NAME LIKE '%{0}%') ",tableFilter));
            }
            SourceCommand.CommandText = sql;
            using (DB2DataAdapter adp = new DB2DataAdapter(SourceCommand))
            {
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;

            }

        }

        internal string CreateSQLSchema(string _tableName)
        {
            string tableName = _tableName.Split('.')[1];
            string schemaname= _tableName.Split('.')[0];

            string sql = string.Format("select  * from {0}.{1} FETCH FIRST 1 ROW ONLY", schemaname,tableName);

            SourceCommand.CommandText = sql;
            DB2DataReader rd = SourceCommand.ExecuteReader();
            DataTable tblSchema = rd.GetSchemaTable();

            string schemaCreate =string.Format(@"
                    if not exists(select * from sys.schemas where name='{0}')
	 EXEC('CREATE SCHEMA {0}')
",schemaname);

            string dropSQL = string.Format(@"if exists(
                                        select S.name, T.NAME from sys.tables T
                                        inner join SYS.schemas  S ON S.schema_id=T.schema_id
                                        WHERE s.NAME='{0}' and t.name='{1}' )
                                         drop table {0}.{1}
                                ", schemaname,tableName);
            string createSql = string.Format(@"Create Table {0}.{1} ",schemaname, tableName);
            string columnsSql = "";
            for (int i = 0; i < tblSchema.Rows.Count; i++)
            {
                string columnSql = GetSqlColumnSchema(tblSchema.Rows[i]);
                columnsSql += "," + columnSql + "\r\n";
            }
            rd.Close();

            return schemaCreate+ dropSQL + createSql + "(" + columnsSql.Substring(1, columnsSql.Length - 1) + ")";
        }

        private string GetSqlColumnSchema(DataRow dataRow)
        {
            string column = dataRow["ColumnName"].ToString();
            if (column.ToUpper() == "TOP" || column.ToUpper() == "END" || column.ToUpper() == "PLAN")
                column = "[" + column + "]";
            DB2Type dataType = (DB2Type)dataRow["ProviderType"];
            string columnType = GetSqlType(dataType);

            if (dataType==DB2Type.VarChar)
            {
                columnType = string.Format("varchar({0})", Convert.ToInt32(dataRow["ColumnSize"]));
            }
            

            return column + " " + columnType + " null";
        }

        private string GetSqlType(DB2Type dataType)
        {
            switch (dataType)
            {
                
                case DB2Type.Binary:
                    return "varbinary(max)";

                case DB2Type.BigInt:
                    return "bigint";

                case DB2Type.Integer:
                    return "int";

                case DB2Type.Blob:
                    return "varbinary(max)";

                case DB2Type.Date:                    
                    return "datetime2";              


                case DB2Type.Decimal:
                    return "decimal(18,6)";

                case DB2Type.Double:
                    return "float";               

                case DB2Type.Float:
                    return "float";

                case DB2Type.Time:
                    return "time";

                case DB2Type.Timestamp:
                    return "timestamp";

                default:
                    return "varchar(max)";
            }
        }


        internal int GetRowCount(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  Count(*) from {0} ", tableName);

            SourceCommand.CommandText = sql;
            DB2DataReader rd = SourceCommand.ExecuteReader();
            rd.Read();
            int rowcount = Convert.ToInt32(rd[0].ToString());
            rd.Close();
            return rowcount;
        }


        internal IDataReader GetDataReader(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  * from {0} ", tableName);

            SourceCommand.CommandText = sql;
            DB2DataReader rd = SourceCommand.ExecuteReader();

            return rd;
        }

        internal DataTable GetDataTable(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  * from {0} ", tableName);

            SourceCommand.CommandText = sql;
            
            using (DB2DataAdapter adp = new DB2DataAdapter(SourceCommand))
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
