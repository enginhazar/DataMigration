using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class OdbcClass
    {


        public OdbcConnection SourceConnection;
        OdbcCommand SourceCommand;
        string getschema = "show table STATUS from ";

        public void Connect(string connectionString)
        {
            SourceConnection = new OdbcConnection(connectionString);
            SourceCommand = new OdbcCommand();
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
            using (OdbcDataAdapter adp = new OdbcDataAdapter(SourceCommand))
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
            OdbcDataReader rd = SourceCommand.ExecuteReader();
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
            if (column.ToUpper() == "TOP")
                column = "[" + column + "]";
            OdbcType dataType = (OdbcType)dataRow["ProviderType"];
            string columnType = GetSqlType(dataType);
            //if (dataType == OdbcType.VarChar || dataType==OdbcType.NVarChar || dataType==OdbcType.NChar || dataType==OdbcType.Char 
            //   )
            //{
            //    if (Convert.ToInt32(dataRow["ColumnSize"]) > 0)
            //        columnType += string.Format("({0})", dataRow["ColumnSize"].ToString());
            //    else
            //        columnType = "varchar(max)";
            //}

            return column + " " + columnType + " null";
        }

        private string GetSqlType(OdbcType dataType)
        {

            switch (dataType)
            {
                case OdbcType.BigInt:
                    return "BigInt";
                case OdbcType.Binary:
                    return "varbinary(MAX)";
                case OdbcType.Bit:
                    return "Bit";
                case OdbcType.Char:
                    return "varchar";
                case OdbcType.Date:
                    return "varchar(20)";
                case OdbcType.DateTime:
                    return "varchar(50)";
                case OdbcType.Decimal:
                    return "decimal";
                case OdbcType.Double:
                    return "decimal";
                case OdbcType.Image:
                    return "varbinary(MAX)";
                case OdbcType.Int:
                    return "int";
                case OdbcType.NChar:
                    return "varchar(max)";
                case OdbcType.NText:
                    return "varchar(max)";
                case OdbcType.NVarChar:
                    return "varchar(max)";
                case OdbcType.Numeric:
                    return "decimal";
                case OdbcType.Real:
                    return "real";
                case OdbcType.SmallDateTime:
                    return "datetime";
                case OdbcType.SmallInt:
                    return "int";
                case OdbcType.Text:
                    return "varchar(max)";
                case OdbcType.Time:
                    return "time";
                case OdbcType.Timestamp:
                    return "timestamp";
                case OdbcType.TinyInt:
                    return "tinyint";
                case OdbcType.UniqueIdentifier:
                    return "varchar(max)";
                case OdbcType.VarBinary:
                             return "varbinary(MAX)";
                case OdbcType.VarChar:
                    return "varchar(max)";
                default:
                    return "varchar(max)";
            }
        }



        internal IDataReader GetDataReader(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  * from {0} ", tableName);
           // string sql = "select * from gelirgode where seri between 605090 and 605095";

            SourceCommand.CommandText = sql;
            OdbcDataReader rd = SourceCommand.ExecuteReader();
            return rd;
        }

        internal DataTable GetDataTable(string tableName)
        {
            tableName = tableName.Trim();
            string sql = string.Format("select  * from {0}", tableName);

            SourceCommand.CommandText = sql;
            using (OdbcDataAdapter adp = new OdbcDataAdapter(SourceCommand))
            {
                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;

            }

        }

    }
}
