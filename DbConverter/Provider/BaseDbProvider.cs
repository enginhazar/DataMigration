using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace DbConverter.Provider
{
    public abstract class BaseDbProvider
    {
        DbConnection _dbConnection;
        DbCommand _dbCommand;
        public readonly DbConverterFrm form;
        public DATABASETYPE DataBaseType { get; set; }

        public abstract string GetTableSQLString
        {
            get;
        }

        public abstract string GetSchemaSQLString(string fromTableName);

        internal abstract string ExistDbSQLString(string dbName);

        internal abstract string CreateDbSQLString(string dbName);

        internal abstract string DeleteDbSQLString(string dbName);



        internal abstract string KillDatabaseAllConnectionSqlString(string dbName);


        internal abstract string ExistSchemaSQLString(string schemaName);

        internal abstract string ExistTableSQLString(Table table);

        internal abstract string TruncateTableSQLString(Table table);


        internal abstract string CreateTableSQLString(Table table);

        internal abstract string DeleteTableSQLString(Table table);

        internal abstract string SelectTableSQLString(Table table);

        internal abstract string CreateTableColumnsSQLString(Table item);

        internal abstract string CreateSchemaSQLString(string schemaName,string roleName);

        internal abstract void GetCreateIndexSQLString(Table table, Index index, ref List<string> createIndexScript);


        internal abstract string GetColumnTypeSQLString(object datataype, TableColumn tableColumn);

        internal abstract string GetIndexSQLString(Table table);

        internal abstract string GetIsNullSQLString(TableColumn tableColumn);

        internal abstract CONVERTADBTYPE GetProviderTypeToConvertDataType(int providerDataType);

        internal abstract object ConvertDataTypeToProviderDataType(CONVERTADBTYPE convertDataType);

        internal abstract void BulkInsertData(Table table, IDataReader reader);




        internal abstract string GetCurrentIdentityValueSQLString(Table table, TableColumn tableColumn);

        internal abstract string SetIdentitySQLString(Table table, TableColumn tableColumn, int identityCurrentValue);




        public DbCommand Command
        {
            get { return _dbCommand; }
        }

        public DbConnection Connection
        {
            get { return _dbConnection; }
        }

        public BaseDbProvider(DbConnection dbConnection, DbCommand dbCommand, DbConverterFrm frm, DATABASETYPE databaseType)
        {
            this._dbConnection = dbConnection;
            this._dbCommand = dbCommand;
            this.form = frm;
            this.DataBaseType = databaseType;
        }

        public void Connect(string connectionString)
        {

            Connection.ConnectionString = connectionString;

            try
            {
                Connection.Open();
            }
            catch (SqlException sqlExcp)
            {

            }
            catch (Exception exp)
            {

                throw;
            }

        }



        //public virtual DbType GetColumnType(int providerType)
        //{
        //    return (DbType)providerType;
        //}

        public virtual List<Table> GetTables()
        {
            DataTable dataTable = GetData(this.GetTableSQLString);
            List<Table> ret = new List<Table>();
            foreach (DataRow item in dataTable.Rows)
            {
                Table table = new Table()
                {
                    TableName = item["TableName"].ToString(),
                    SchemaName = item["SchemaName"].ToString(),
                    RowCount = int.Parse(item["TotalRowCount"].ToString()),
                    SourceDataBaseType = this.DataBaseType

                };

                DataTable columnSchemaDataTable = GetSchema($"{table.SchemaName}.{table.TableName}");
                List<TableColumn> tableColumns = GetSchemaColumn(columnSchemaDataTable);

                //DataTable tableIndex = GetIndexList(table);

                //GetIndexColumns(tableIndex, ref table);

                table.TableColumns.AddRange(tableColumns);
                ret.Add(table);

            }
            return ret;
        }

        public void GetIndexColumns(ref Table table)
        {
            DataTable tableIndex = GetIndexList(table);
            foreach (DataRow item in tableIndex.Rows)
            {
                string indexName = item["IndexName"].ToString();
                Index index;
                if (!table.Indexes.Keys.Contains(indexName))
                {
                    index = new Index();
                    index.IndexName = indexName;
                    index.IndexType = INDEXTYPE.NONECLUSTER;
                    if (bool.Parse(item["is_primary_key"].ToString()))
                        index.IndexType = INDEXTYPE.PRIMARYKEY;

                    table.Indexes.Add(indexName, index);
                }
                else
                {
                    index = table.Indexes[indexName];
                }

                string columnName = item["ColumnName"].ToString();

                if (!index.IndexColumnList.ContainsKey(columnName))
                    index.IndexColumnList.Add(columnName, columnName);

            }
        }

        private DataTable GetIndexList(Table table)
        {
            string indexSQLString = this.GetIndexSQLString(table);
            return GetData(indexSQLString);
        }

        private List<TableColumn> GetSchemaColumn(DataTable columnSchemaDataTable)
        {
            List<TableColumn> ret = new List<TableColumn>();
            foreach (DataRow item in columnSchemaDataTable.Rows)
            {
                TableColumn tableColumn = new TableColumn()
                {
                    ColumnName = item["ColumnName"].ToString(),
                    ColumnSize = int.Parse(item["ColumnSize"].ToString()),
                    DataTypeName = item["DataTypeName"].ToString(),
                    IsAllowNull = bool.Parse(item["AllowDbNull"].ToString()),
                    IsIdentity = bool.Parse(item["IsAutoIncrement"].ToString()),
                    IsAutoIncrement = bool.Parse(item["IsAutoIncrement"].ToString()),
                    ProviderSpecificDataType = item["ProviderSpecificDataTYpe"].ToString(),
                    NumericPrecision = int.Parse(item["NumericPrecision"].ToString()),
                    NumvericScale = int.Parse(item["NumericScale"].ToString()),
                    ColumnType = GetProviderTypeToConvertDataType(int.Parse(item["ProviderType"].ToString())),
                    DbProviderType = int.Parse(item["ProviderType"].ToString())

                };
                ret.Add(tableColumn);
            }
            return ret;
        }

        internal bool IsExistingSchema(Table item)
        {
            DataTable dataTable = GetData(this.ExistSchemaSQLString(item.SchemaName));
            bool ret = dataTable.Rows.Count > 0 ? true : false;
            if (ret)
                form.LogYaz($@"{item.SchemaName} Schema is Exists");
            return ret;
        }

        internal void CreateSchema(Table item,string roleName)
        {
            ExecuteNoneQuery(this.CreateSchemaSQLString(item.SchemaName,roleName));
            form.LogYaz($@"{item.SchemaName} Schema is Created");
        }

        internal DataTable GetData(string sqlString)
        {
            DbProviderFactory fac = DbProviderFactories.GetFactory(this.Connection);


            using (DbDataAdapter adp = fac.CreateDataAdapter())
            {
                this.Command.CommandText = sqlString;
                this.Command.Connection = this.Connection;
                adp.SelectCommand = this.Command;

                DataTable tbl = new DataTable();
                adp.Fill(tbl);
                return tbl;
            }
        }

        internal void DeleteTable(Table item)
        {
            ExecuteNoneQuery(this.DeleteTableSQLString(item));
            form.LogYaz($@"{item.SchemaName}.{item.TableName} Table is Deleted");
        }




        internal IDataReader GetSourceTableDataReader(Table table)
        {
            IDataReader rdr = GetDataReader(this.SelectTableSQLString(table));

            form.LogYaz($@"{table.SchemaName}.{table.TableName} Table is Reader Started");
            return rdr;
        }
        internal IDataReader GetDataReader(string sql)
        {
            Command.CommandText = sql;
            Command.Connection = Connection;
            return Command.ExecuteReader();
        }

        private DataTable GetSchema(string fromTableName)
        {
            if (Connection.State==ConnectionState.Closed)
                Connection.Open();
            Command.CommandText = GetSchemaSQLString(fromTableName);
            Command.Connection = Connection;
            DataTable tblSchema;
            using (DbDataReader rdr = Command.ExecuteReader())
            {
                tblSchema = rdr.GetSchemaTable();
            }
            return tblSchema;

        }

        internal void CreateTableIndex(Table table)
        {
            foreach (Index index in table.Indexes.Values)
            {
                List<string> createIndexString = new List<string>();
                GetCreateIndexSQLString(table, index, ref createIndexString);

                foreach (string item in createIndexString)
                {
                    ExecuteNoneQuery(item.Replace("-", ""));
                }
                form.LogYaz($@"{table.SchemaName}.{table.TableName} on {index.IndexName} index is Created");

            }
        }

        internal void TruncateTable(Table table)
        {
            ExecuteNoneQuery(this.TruncateTableSQLString(table));
            form.LogYaz($@"{table.TableName} Table is Created");
        }

        internal void CreateTable(Table table)
        {
            ExecuteNoneQuery(this.CreateTableSQLString(table));
            form.LogYaz($@"{table.SchemaName}.{table.TableName} Table is Created");
        }


        internal void SetTargetDatabaseConnection(string databaseName)
        {
            this.Connection.ChangeDatabase(databaseName);
        }

        internal bool IsExistsTable(Table table)
        {
            DataTable dataTable = GetData(this.ExistTableSQLString(table));
            bool ret = dataTable.Rows.Count > 0 ? true : false;
            if (ret)
                form.LogYaz($@"{table.SchemaName}.{table.TableName} Table is Exists");
            return ret;
        }



        private void ExecuteNoneQuery(string sql)
        {

            Command.CommandText = sql;
            Command.Connection = Connection;
            Command.ExecuteNonQuery();
        }

        public bool ExistsDB(string dbName)
        {
            DataTable dataTable = GetData(this.ExistDbSQLString(dbName));
            bool ret = dataTable.Rows.Count > 0 ? true : false;
            if (ret)
                form.LogYaz($@"{dbName} Database is Exists");
            return ret;

        }

        public void CreateDB(string dbName)
        {
            ExecuteNoneQuery(this.CreateDbSQLString(dbName));
            form.LogYaz($@"{dbName} Database is Created");
        }
        internal void DeleteDB(string dbName)
        {
            ExecuteNoneQuery(this.DeleteDbSQLString(dbName));
            form.LogYaz($@"{dbName} Database is Deleted");
        }

        internal void DatabaseAllConnectionKill(string dbName)
        {
            ExecuteNoneQuery(this.KillDatabaseAllConnectionSqlString(dbName));
            form.LogYaz($@"{dbName} Database All Connection Killed");
        }
        internal object GetValue(object value, CONVERTADBTYPE sourceType)
        {
            object retValue;
            if (value==DBNull.Value || value==null)
                return null;
            switch (sourceType)
            {
                case CONVERTADBTYPE.DBCONVERTBIGINT:
                    retValue=Convert.ToDouble(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTBINARY:
                    retValue=(byte[])value;
                    break;
                case CONVERTADBTYPE.DBCONVERTBIT:
                    retValue=Convert.ToBoolean(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTCHAR:
                    retValue=Convert.ToString(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTDATE:
                    retValue=Convert.ToDateTime(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTDATETIME:
                    retValue=Convert.ToDateTime(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTDATETIME2:
                    retValue=Convert.ToDateTime(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTDATETIMEOFFSET:
                    retValue=Convert.ToDateTime(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTDECIMAL:
                    retValue=Convert.ToDecimal(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTFLOAT:
                    retValue=Convert.ToSingle(value);

                    break;
                case CONVERTADBTYPE.DBCONVERTGEOGRAPHT:
                    retValue=(byte[])value;

                    break;
                case CONVERTADBTYPE.DBCONVERTGEOMETRY:
                    retValue=(byte[])value;

                    break;
                case CONVERTADBTYPE.DBCONVERTHIERARCHYID:
                    retValue=Convert.ToInt64(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTIMAGE:
                    retValue=(byte[])value;
                    break;
                case CONVERTADBTYPE.DBCONVERTINT:
                    retValue=Convert.ToInt64(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTMONEY:
                    retValue=Convert.ToDecimal(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTNCHAR:
                    retValue=Convert.ToString(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTNTEXT:
                    retValue=Convert.ToString(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTNVARCHAR:
                    retValue=Convert.ToString(value).Replace("\0","");
                    
                    break;
                case CONVERTADBTYPE.DBCONVERTNVARCHARMAX:
                    retValue=Convert.ToString(value).Replace("\0", "");
                    //retValue=Convert.ToString(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTREAL:
                    retValue=Convert.ToSingle(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTSMALLDATETIME:
                    retValue=Convert.ToDateTime(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTSMALLINT:
                    retValue=Convert.ToInt16(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTSMALLMONEY:
                    retValue=Convert.ToDecimal(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTSQLVARIANT:
                    retValue=(byte[])value;
                    break;
                case CONVERTADBTYPE.DBCONVERTTEXT:
                    retValue=Convert.ToString(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTTIME:
                    retValue=(TimeSpan)(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTTIMESTAMP:
                    retValue=(byte[])value;
                    break;
                case CONVERTADBTYPE.DBCONVERTTINYINT:
                    retValue=Convert.ToInt64(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTUNIQUEIDENTIFIER:
                    retValue=(byte[])value;
                    break;
                case CONVERTADBTYPE.DBCONVERTVARBINARY:
                    retValue=(byte[])value;
                    break;
                case CONVERTADBTYPE.DBCONVERTVARBINARYMAX:
                    retValue=(byte[])value;
                    break;
                case CONVERTADBTYPE.DBCONVERTVARCHAR:
                    retValue=Convert.ToString(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTVARCHARMAX:
                    retValue=Convert.ToString(value);
                    break;
                case CONVERTADBTYPE.DBCONVERTXML:
                    retValue=Convert.ToString(value);
                    break;
                default:
                    retValue=value;
                    break;
            }
            return retValue;
        }

        internal void SetIdentityValue(Table table)
        {
            TableColumn identityColumn = table.TableColumns.Where(x => x.IsIdentity == true).FirstOrDefault();

            if (identityColumn == null)
                return;

            int currnetMaxValue = GetCurrentIdentityValue(table, identityColumn);

            if (currnetMaxValue > 1)
            {
                string setIdentityCurrentValueSqlString = SetIdentitySQLString(table, identityColumn, currnetMaxValue);
                ExecuteNoneQuery(setIdentityCurrentValueSqlString);

                form.LogYaz($@"{table.SchemaName}.{table.TableName} on {identityColumn.ColumnName} identity Current value {currnetMaxValue}");
            }
        }

        private int GetCurrentIdentityValue(Table table, TableColumn tableColumn)
        {
            string sqlIdentitSqlString = GetCurrentIdentityValueSQLString(table, tableColumn);

            DataTable identityMaxCurrentValueTable = GetData(sqlIdentitSqlString);
            if (identityMaxCurrentValueTable.Rows.Count == 0 || identityMaxCurrentValueTable.Rows[0][0] == DBNull.Value)
                return 1;
            else
                return (int)identityMaxCurrentValueTable.Rows[0][0];


        }

    }


    public class Table
    {
        List<TableColumn> tableColumns;
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public int RowCount { get; set; }
        SortedList<string, Index> _indexes;
        public SortedList<string, Index> Indexes
        {
            get
            {
                if (_indexes == null)
                    _indexes = new SortedList<string, Index>();
                return _indexes;

            }
        }


        public List<TableColumn> TableColumns
        {
            get
            {
                if (tableColumns == null)
                    tableColumns = new List<TableColumn>();
                return tableColumns;
            }
        }


        public DATABASETYPE SourceDataBaseType { get; set; }

        public string GetColumnsString()
        {
            StringBuilder columnString = new StringBuilder();
            foreach (TableColumn tableColumn in TableColumns)
            {
                columnString.Append($",{tableColumn.ColumnName.ToLower().Replace("ı", "i")}");
            }

            return columnString.ToString().Substring(1, columnString.ToString().Length - 1);
        }

    }

    public class Index
    {
        public string IndexName { get; set; }
        public INDEXTYPE IndexType { get; set; }
        Dictionary<string, string> _indexColumnList;
        public Dictionary<string, string> IndexColumnList
        {
            get
            {
                if (_indexColumnList == null)
                    _indexColumnList = new Dictionary<string, string>();
                return _indexColumnList;
            }
        }
    }
    public enum INDEXTYPE
    {
        PRIMARYKEY = 1,
        NONECLUSTER = 2
    }
    public class TableColumn
    {
        public string ColumnName { get; set; }
        public CONVERTADBTYPE ColumnType { get; set; }
        public int ColumnSize { get; set; }
        public int NumericPrecision { get; set; }
        public int NumvericScale { get; set; }
        public bool IsAllowNull { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsAutoIncrement { get; set; }
        public string ProviderSpecificDataType { get; set; }
        public string DataTypeName { get; set; }
        public int DbProviderType { get; set; }

    }


    public enum CONVERTADBTYPE
    {
        DBCONVERTBIGINT = 1,
        DBCONVERTBINARY = 2,
        DBCONVERTBIT = 3,
        DBCONVERTCHAR = 4,
        DBCONVERTDATE = 5,
        DBCONVERTDATETIME = 6,
        DBCONVERTDATETIME2 = 7,
        DBCONVERTDATETIMEOFFSET = 8,
        DBCONVERTDECIMAL = 9,
        DBCONVERTFLOAT = 10,
        DBCONVERTGEOGRAPHT = 11,
        DBCONVERTGEOMETRY = 12,
        DBCONVERTHIERARCHYID = 13,
        DBCONVERTIMAGE = 14,
        DBCONVERTINT = 15,
        DBCONVERTMONEY = 16,
        DBCONVERTNCHAR = 17,
        DBCONVERTNTEXT = 18,
        DBCONVERTNVARCHAR = 19,
        DBCONVERTNVARCHARMAX = 20,
        DBCONVERTREAL = 21,
        DBCONVERTSMALLDATETIME = 22,
        DBCONVERTSMALLINT = 23,
        DBCONVERTSMALLMONEY = 24,
        DBCONVERTSQLVARIANT = 25,
        DBCONVERTTEXT = 26,
        DBCONVERTTIME = 27,
        DBCONVERTTIMESTAMP = 28,
        DBCONVERTTINYINT = 29,
        DBCONVERTUNIQUEIDENTIFIER = 30,
        DBCONVERTVARBINARY = 31,
        DBCONVERTVARBINARYMAX = 32,
        DBCONVERTVARCHAR = 33,
        DBCONVERTVARCHARMAX = 34,
        DBCONVERTXML = 35
    }

}
