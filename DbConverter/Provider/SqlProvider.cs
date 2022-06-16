using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DbConverter.Provider
{
    public class SqlProvider : BaseDbProvider
    {


        public SqlProvider(DbConverterFrm frm,DATABASETYPE databaseType)
: base(new SqlConnection(), new SqlCommand(),frm, databaseType)
        {

        }

        public override string GetTableSQLString => @"SELECT  SCHEMA_NAME(schema_id) AS [SchemaName],
                                                [Tables].name AS [TableName],
                                                SUM([Partitions].[rows]) AS [TotalRowCount]
                                                FROM sys.tables AS [Tables]
                                                JOIN sys.partitions AS [Partitions]
                                                ON [Tables].[object_id] = [Partitions].[object_id]
                                                AND [Partitions].index_id IN ( 0, 1 )
                                                where  Tables.name not like '%sysdiagrams%'
                                               --and tables.name = 'AdresDefteri'
                                              --  and  left(SCHEMA_NAME(schema_id),1) ='AdresDefteri'
                                                GROUP BY SCHEMA_NAME(schema_id), [Tables].name
                                                ORDER BY 1,2";

        internal override string ExistDbSQLString(string dbName)
        {
            throw new System.NotImplementedException();
        }

        public override string GetSchemaSQLString(string fromTableName)
        {
            return $@"SELECT TOP 1 * FROM {fromTableName} ";
        }

        internal override string CreateDbSQLString(string dbName)
        {
            throw new System.NotImplementedException();
        }

        internal override string DeleteDbSQLString(string dbName)
        {
            throw new System.NotImplementedException();
        }

        internal override string KillDatabaseAllConnectionSqlString(string dbName)
        {
            throw new System.NotImplementedException();
        }



        internal override string CreateTableSQLString(Table item)
        {
            throw new System.NotImplementedException();
        }

        internal override string CreateTableColumnsSQLString(Table item)
        {
            throw new System.NotImplementedException();
        }

        internal override string GetColumnTypeSQLString(object datataype, TableColumn tableColumn)
        {
            throw new System.NotImplementedException();
        }

        internal override string GetIsNullSQLString(TableColumn tableColumn)
        {
            throw new System.NotImplementedException();
        }

        internal override CONVERTADBTYPE GetProviderTypeToConvertDataType(int providerDataType)
        {
            SqlDbType sqlDbType = (SqlDbType)providerDataType;
            CONVERTADBTYPE retConvertDBType;
            switch (sqlDbType)
            {
                case SqlDbType.BigInt:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTBIGINT;
                    break;
                case SqlDbType.Binary:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTBINARY;
                    break;
                case SqlDbType.Bit:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTBIT;
                    break;
                case SqlDbType.Char:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTCHAR;
                    break;
                case SqlDbType.DateTime:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTDATETIME;
                    break;
                case SqlDbType.Decimal:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTDECIMAL;
                    break;
                case SqlDbType.Float:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTFLOAT;
                    break;
                case SqlDbType.Image:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTIMAGE;
                    break;
                case SqlDbType.Int:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTINT;
                    break;
                case SqlDbType.Money:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTMONEY;
                    break;
                case SqlDbType.NChar:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTNCHAR;
                    break;
                case SqlDbType.NText:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTNTEXT;
                    break;
                case SqlDbType.NVarChar:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTNVARCHAR;
                    break;
                case SqlDbType.Real:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTREAL;
                    break;
                case SqlDbType.UniqueIdentifier:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTUNIQUEIDENTIFIER;
                    break;
                case SqlDbType.SmallDateTime:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTDATE;
                    break;
                case SqlDbType.SmallInt:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTSMALLINT;
                    break;
                case SqlDbType.SmallMoney:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTSMALLMONEY;
                    break;
                case SqlDbType.Text:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTTEXT;
                    break;
                case SqlDbType.Timestamp:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTTIMESTAMP;
                    break;
                case SqlDbType.TinyInt:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTTINYINT;
                    break;
                case SqlDbType.VarBinary:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTVARBINARY;
                    break;
                case SqlDbType.VarChar:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTVARCHAR;
                    break;
                case SqlDbType.Variant:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTSQLVARIANT;
                    break;
                case SqlDbType.Xml:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTXML;
                    break;
                case SqlDbType.Udt:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTNVARCHAR;
                    break;
                case SqlDbType.Structured:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTNVARCHAR;
                    break;
                case SqlDbType.Date:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTDATE;
                    break;
                case SqlDbType.Time:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTTIME;
                    break;
                case SqlDbType.DateTime2:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTDATETIME2;
                    break;
                case SqlDbType.DateTimeOffset:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTDATETIMEOFFSET;
                    break;
                default:
                    retConvertDBType = CONVERTADBTYPE.DBCONVERTVARCHAR;
                    break;
            }
            return retConvertDBType;
        }

        internal override object ConvertDataTypeToProviderDataType(CONVERTADBTYPE convertDataType)
        {
            throw new System.NotImplementedException();
        }

        internal override string DeleteTableSQLString(Table table)
        {
            throw new System.NotImplementedException();
        }

        internal override string ExistTableSQLString(Table table)
        {
            throw new System.NotImplementedException();
        }

        internal override string ExistSchemaSQLString(string schemaName)
        {
            throw new System.NotImplementedException();
        }

        internal override string CreateSchemaSQLString(string schemaName, string roleName)
        {
            throw new System.NotImplementedException();
        }

        internal override string SelectTableSQLString(Table table)
        {
             return $@"SELECT top 10 * FROM {table.SchemaName}.{table.TableName} ";
            
        }

        internal override void BulkInsertData(Table table, IDataReader reader)
        {
            throw new System.NotImplementedException();
        }

        internal override string TruncateTableSQLString(Table table)
        {
            throw new System.NotImplementedException();
        }

        internal override string GetIndexSQLString(Table table)
        {
            return $@"SELECT 
     TableName = t.name,
     IndexName = ind.name,
     IndexId = ind.index_id,
     ColumnId = ic.index_column_id,
     ColumnName = col.name,
	 ind.is_primary_key,
	 ind.is_unique,
	 ind.type_desc
     

FROM 
     sys.indexes ind 
INNER JOIN 
     sys.index_columns ic ON  ind.object_id = ic.object_id and ind.index_id = ic.index_id 
INNER JOIN 
     sys.columns col ON ic.object_id = col.object_id and ic.column_id = col.column_id 
INNER JOIN 
     sys.tables t ON ind.object_id = t.object_id 
WHERE 
      ind.is_unique_constraint = 0 
     AND t.is_ms_shipped = 0 
    and t.name='{table.TableName}' and SCHEMA_NAME(schema_id)='{table.SchemaName}'
ORDER BY 
     t.name, ind.name, ind.index_id, ic.index_column_id;";
        }

        internal override void GetCreateIndexSQLString(Table table, Index index, ref List<string> createIndexScript)
        {
            throw new System.NotImplementedException();
        }

        internal override string GetCurrentIdentityValueSQLString(Table table, TableColumn tableColumn)
        {
            throw new System.NotImplementedException();
        }

        internal override string SetIdentitySQLString(Table table, TableColumn tableColumn, int identityCurrentValue)
        {
            throw new System.NotImplementedException();
        }
    }


 

}
