using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;
using Npgsql;
using NpgsqlTypes;

namespace DbConverter.Provider
{
    public class PostgresProvider : BaseDbProvider
    {


        public PostgresProvider(DbConverterFrm frm, DATABASETYPE databaseType)
: base(new NpgsqlConnection(), new NpgsqlCommand(), frm, databaseType)
        {

        }

        public override string GetTableSQLString => @"SELECT SCHEMA_NAME(schema_id) AS [SchemaName],
                                                [Tables].name AS [TableName],
                                                SUM([Partitions].[rows]) AS [TotalRowCount]
                                                FROM sys.tables AS [Tables]
                                                JOIN sys.partitions AS [Partitions]
                                                ON [Tables].[object_id] = [Partitions].[object_id]
                                                AND [Partitions].index_id IN ( 0, 1 )
                                                GROUP BY SCHEMA_NAME(schema_id), [Tables].name
                                                ORDER BY 1,2";

        internal override string ExistDbSQLString(string dbName)
        {

            return $@" SELECT 1 datname from pg_catalog.pg_database WHERE datname='{dbName}';";
        }

        public override string GetSchemaSQLString(string fromTableName)
        {
            return $@"SELECT TOP 1 * FROM {fromTableName} ";
        }

        internal override string CreateDbSQLString(string dbName)
        {
            return $@"CREATE DATABASE {dbName} ";
            //WITH ENCODING = 'UTF8'  LC_COLLATE = 'en_US.UTF-8' LC_CTYPE = 'en_US.UTF-8'";

        }

        internal override string DeleteDbSQLString(string dbName)
        {
            return $@"DROP DATABASE {dbName} ";
        }

        internal override string KillDatabaseAllConnectionSqlString(string dbName)
        {
            return $@" SELECT pg_terminate_backend(pg_stat_activity.pid)
FROM pg_stat_activity
WHERE datname = '{dbName}'
  AND pid<> pg_backend_pid();";
        }

        internal override string ExistTableSQLString(Table table)
        {

            return $@"SELECT*
   FROM   pg_tables
   WHERE    schemaname='{table.SchemaName.ToLower().Replace("ı", "i")}' and tablename = '{table.TableName.ToLower().Replace("ı", "i")}'";
        }

        internal override string CreateTableSQLString(Table table)
        {

            string createSql = $@" CREATE TABLE {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i")}(
                            {CreateTableColumnsSQLString(table)}
                                )";
            return createSql;




        }

        internal override string CreateTableColumnsSQLString(Table table)
        {
            StringBuilder columnString = new StringBuilder();


            foreach (TableColumn tableColumn in table.TableColumns)
            {
                if (tableColumn.IsIdentity)
                    columnString.AppendLine($",{tableColumn.ColumnName.Replace(" ", "").ToLower().Replace("ı", "i")} SERIAL  {GetIsNullSQLString(tableColumn)}");
                else
                    columnString.AppendLine($",{tableColumn.ColumnName.Replace(" ", "").ToLower().Replace("ı", "i")} {GetColumnTypeSQLString(ConvertDataTypeToProviderDataType(tableColumn.ColumnType), tableColumn)} {GetIsNullSQLString(tableColumn)}");
            }
            return columnString.ToString().Substring(1, columnString.ToString().Length - 1);
        }

        internal override string GetColumnTypeSQLString(object datataype, TableColumn tableColumn)
        {
            string retType = ((NpgsqlDbType)datataype).ToString();
            switch ((NpgsqlDbType)datataype)
            {
                case NpgsqlDbType.Bigint:
                    break;
                case NpgsqlDbType.Double:
                    break;
                case NpgsqlDbType.Integer:
                    break;
                case NpgsqlDbType.Numeric:
                    retType += $"({tableColumn.NumericPrecision.ToString()},{tableColumn.NumvericScale.ToString()})";
                    break;
                case NpgsqlDbType.Real:
                    break;
                case NpgsqlDbType.Smallint:
                    break;
                case NpgsqlDbType.Money:
                    break;
                case NpgsqlDbType.Boolean:
                    break;
                case NpgsqlDbType.Box:
                    break;
                case NpgsqlDbType.Circle:
                    break;
                case NpgsqlDbType.Line:
                    break;
                case NpgsqlDbType.LSeg:
                    break;
                case NpgsqlDbType.Path:
                    break;
                case NpgsqlDbType.Point:
                    break;
                case NpgsqlDbType.Polygon:
                    break;
                case NpgsqlDbType.Char:
                    retType += $"({tableColumn.ColumnSize.ToString()})";
                    if (tableColumn.ColumnSize > 8000)
                        retType = NpgsqlDbType.Text.ToString();

                    break;
                case NpgsqlDbType.Text:
                    break;
                case NpgsqlDbType.Varchar:
                    retType += $"({tableColumn.ColumnSize.ToString()})";
                    if (tableColumn.ColumnSize > 8000)
                        retType = NpgsqlDbType.Text.ToString();
                    break;
                case NpgsqlDbType.Name:
                    break;
                case NpgsqlDbType.Citext:
                    break;
                case NpgsqlDbType.InternalChar:
                    break;
                case NpgsqlDbType.Bytea:
                    break;
                case NpgsqlDbType.Date:
                    break;
                case NpgsqlDbType.Time:
                    break;
                case NpgsqlDbType.Timestamp:
                    retType = "TIMESTAMP(3)";
                    break;
                case NpgsqlDbType.TimestampTZ:
                    break;
                case NpgsqlDbType.Interval:
                    break;
                case NpgsqlDbType.TimeTZ:
                    break;
                case NpgsqlDbType.Abstime:
                    break;
                case NpgsqlDbType.Inet:
                    break;
                case NpgsqlDbType.Cidr:
                    break;
                case NpgsqlDbType.MacAddr:
                    break;
                case NpgsqlDbType.MacAddr8:
                    break;
                case NpgsqlDbType.Bit:
                    break;
                case NpgsqlDbType.Varbit:
                    break;
                case NpgsqlDbType.TsVector:
                    break;
                case NpgsqlDbType.TsQuery:
                    break;
                case NpgsqlDbType.Regconfig:
                    break;
                case NpgsqlDbType.Uuid:
                    break;
                case NpgsqlDbType.Xml:
                    break;
                case NpgsqlDbType.Json:
                    break;
                case NpgsqlDbType.Jsonb:
                    break;
                case NpgsqlDbType.Hstore:
                    break;
                case NpgsqlDbType.Array:
                    break;
                case NpgsqlDbType.Range:
                    break;
                case NpgsqlDbType.Refcursor:
                    break;
                case NpgsqlDbType.Oidvector:
                    break;
                case NpgsqlDbType.Int2Vector:
                    break;
                case NpgsqlDbType.Oid:
                    break;
                case NpgsqlDbType.Xid:
                    break;
                case NpgsqlDbType.Cid:
                    break;
                case NpgsqlDbType.Regtype:
                    break;
                case NpgsqlDbType.Tid:
                    break;
                case NpgsqlDbType.Unknown:
                    break;
                case NpgsqlDbType.Geometry:
                    break;
                case NpgsqlDbType.Geography:
                    break;
                default:
                    break;
            }

            return retType;


        }

        internal override string GetIsNullSQLString(TableColumn tableColumn)
        {
            return !tableColumn.IsAllowNull ? "NOT NULL" : "";
        }



        internal override CONVERTADBTYPE GetProviderTypeToConvertDataType(int providerDataType)
        {
            throw new System.NotImplementedException();
        }

        internal override object ConvertDataTypeToProviderDataType(CONVERTADBTYPE convertDataType)
        {
            NpgsqlDbType npgsqlDbType;
            switch (convertDataType)
            {
                case CONVERTADBTYPE.DBCONVERTBIGINT:
                    npgsqlDbType = NpgsqlDbType.Bigint;

                    break;
                case CONVERTADBTYPE.DBCONVERTBINARY:
                    npgsqlDbType = NpgsqlDbType.Array;
                    break;
                case CONVERTADBTYPE.DBCONVERTBIT:
                    npgsqlDbType = NpgsqlDbType.Bit;
                    break;
                case CONVERTADBTYPE.DBCONVERTCHAR:
                    npgsqlDbType = NpgsqlDbType.Char;
                    break;
                case CONVERTADBTYPE.DBCONVERTDATE:
                    npgsqlDbType = NpgsqlDbType.Date;
                    break;
                case CONVERTADBTYPE.DBCONVERTDATETIME:
                    npgsqlDbType = NpgsqlDbType.Timestamp;

                    break;
                case CONVERTADBTYPE.DBCONVERTDATETIME2:
                    npgsqlDbType = NpgsqlDbType.TimeTz;
                    break;
                case CONVERTADBTYPE.DBCONVERTDATETIMEOFFSET:
                    npgsqlDbType = NpgsqlDbType.TimeTz;
                    break;
                case CONVERTADBTYPE.DBCONVERTDECIMAL:
                    npgsqlDbType = NpgsqlDbType.Numeric;
                    break;
                case CONVERTADBTYPE.DBCONVERTFLOAT:
                    npgsqlDbType = NpgsqlDbType.Real;
                    break;
                case CONVERTADBTYPE.DBCONVERTGEOGRAPHT:
                    npgsqlDbType = NpgsqlDbType.Geography;
                    break;
                case CONVERTADBTYPE.DBCONVERTGEOMETRY:
                    npgsqlDbType = NpgsqlDbType.Geometry;
                    break;
                case CONVERTADBTYPE.DBCONVERTHIERARCHYID:
                    npgsqlDbType = NpgsqlDbType.Text;
                    break;
                case CONVERTADBTYPE.DBCONVERTIMAGE:
                    npgsqlDbType = NpgsqlDbType.Bytea;
                    break;
                case CONVERTADBTYPE.DBCONVERTINT:
                    npgsqlDbType = NpgsqlDbType.Integer;
                    break;
                case CONVERTADBTYPE.DBCONVERTMONEY:
                    npgsqlDbType = NpgsqlDbType.Money;
                    break;
                case CONVERTADBTYPE.DBCONVERTNCHAR:
                    npgsqlDbType = NpgsqlDbType.Varchar; ;
                    break;
                case CONVERTADBTYPE.DBCONVERTNTEXT:
                    npgsqlDbType = NpgsqlDbType.Text;
                    break;
                case CONVERTADBTYPE.DBCONVERTNVARCHAR:
                    npgsqlDbType = NpgsqlDbType.Varchar;
                    break;
                case CONVERTADBTYPE.DBCONVERTNVARCHARMAX:
                    npgsqlDbType = NpgsqlDbType.Varchar;
                    break;
                case CONVERTADBTYPE.DBCONVERTREAL:
                    npgsqlDbType = NpgsqlDbType.Real;
                    break;
                case CONVERTADBTYPE.DBCONVERTSMALLDATETIME:
                    npgsqlDbType = NpgsqlDbType.TimeTZ;
                    break;
                case CONVERTADBTYPE.DBCONVERTSMALLINT:
                    npgsqlDbType = NpgsqlDbType.Smallint;
                    break;
                case CONVERTADBTYPE.DBCONVERTSMALLMONEY:
                    npgsqlDbType = NpgsqlDbType.Money;
                    break;
                case CONVERTADBTYPE.DBCONVERTSQLVARIANT:
                    npgsqlDbType = NpgsqlDbType.Varchar;
                    break;
                case CONVERTADBTYPE.DBCONVERTTEXT:
                    npgsqlDbType = NpgsqlDbType.Text;
                    break;
                case CONVERTADBTYPE.DBCONVERTTIME:
                    npgsqlDbType = NpgsqlDbType.Time;
                    break;
                case CONVERTADBTYPE.DBCONVERTTIMESTAMP:
                    npgsqlDbType = NpgsqlDbType.Bytea;
                    break;
                case CONVERTADBTYPE.DBCONVERTTINYINT:
                    npgsqlDbType = NpgsqlDbType.Integer;
                    break;
                case CONVERTADBTYPE.DBCONVERTUNIQUEIDENTIFIER:
                    npgsqlDbType = NpgsqlDbType.Uuid;
                    break;
                case CONVERTADBTYPE.DBCONVERTVARBINARY:
                    npgsqlDbType = NpgsqlDbType.Bytea;
                    break;
                case CONVERTADBTYPE.DBCONVERTVARBINARYMAX:
                    npgsqlDbType = NpgsqlDbType.Bytea;
                    break;
                case CONVERTADBTYPE.DBCONVERTVARCHAR:
                    npgsqlDbType = NpgsqlDbType.Varchar;
                    break;
                case CONVERTADBTYPE.DBCONVERTVARCHARMAX:
                    npgsqlDbType = NpgsqlDbType.Varchar;
                    break;
                case CONVERTADBTYPE.DBCONVERTXML:
                    npgsqlDbType = NpgsqlDbType.Xml;
                    break;
                default:
                    npgsqlDbType = NpgsqlDbType.Varchar;
                    break;
            }
            return npgsqlDbType;
        }

        internal override string DeleteTableSQLString(Table table)
        {

            return $@"  set lc_messages = 'C';
                        DROP TABLE {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i")};";
        }

        internal override string ExistSchemaSQLString(string schemaName)
        {


            return $@"select nspname from pg_catalog.pg_namespace where nspname='{schemaName.ToLower().Replace("ı", "i")}'";
        }

        internal override string CreateSchemaSQLString(string schemaName, string roleName)
        {
            return $@"CREATE SCHEMA {schemaName} AUTHORIZATION {roleName};
            GRANT ALL ON SCHEMA {schemaName} TO PUBLIC;";
        }

        internal override string SelectTableSQLString(Table table)
        {
            throw new System.NotImplementedException();
        }

        internal override void BulkInsertData(Table table, IDataReader reader)
        {

            //Nps
            //COPY arge.projesurec (argeprojesurecid,surecadi,tstamp,isdefault,tasimatablo,tasimaid,createdate,createkullaniciid,updatedate,updatekullaniciid) FROM STDIN (FORMAT BINARY)
            var x = $"COPY {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i").Replace(" ", "")} ({table.GetColumnsString()}) FROM STDIN (FORMAT BINARY)";
            try
            {
                 using (var writer = ((NpgsqlConnection)this.Connection).BeginBinaryImport($"COPY {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i").Replace(" ", "")} ({table.GetColumnsString().Replace(" ", "")}) FROM STDIN (FORMAT BINARY)"))
                //using (var writer = ((NpgsqlConnection)this.Connection).BeginBinaryImport("COPY genel.din (geneldinid) FROM STDIN (FORMAT BINARY)"))
                {
                    object[] values = new object[reader.FieldCount];
                    int count = 0;
                    while (reader.Read())
                    {

                        reader.GetValues(values);
                        if (count >0)
                            break;
                   
                        if (count>0 && count % 10000 == 0)
                        {
                            form.LogYaz($@"{table.SchemaName}.{table.TableName} Table {count} rows affected");
                           
                        }
                        writer.StartRow();

                        for (int i = 0; i < values.Length; i++)
                        {
                            TableColumn tableColumn = table.TableColumns[i];
                            object value = GetValue(values[i], tableColumn.ColumnType);
                            if (tableColumn.IsIdentity)
                            {
                               // value=Convert.ToInt16(value);
                                writer.Write(value, NpgsqlDbType.Integer);
                            }
                            else
                            {
                                if (value==null ||  value == DBNull.Value)
                                    writer.WriteNull();
                                else

                                    writer.Write(value, (NpgsqlDbType)ConvertDataTypeToProviderDataType(tableColumn.ColumnType));
                            }
                        }

                        count++;

                    }
                    writer.Complete();
                    reader.Close();

                    form.LogYaz($@"{table.SchemaName}.{table.TableName} Table {count} rows affected");
                }
            }
            catch (NpgsqlException npex)
            {
                form.LogYaz($"Message : {npex.Message}\r\n Data={npex.Data["Detail"]}");
                throw npex;
            }


        }



        internal override string TruncateTableSQLString(Table table)
        {
            return $@"TRUNCATE TABLE {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i")}";
        }

        internal override string GetIndexSQLString(Table table)
        {
            throw new NotImplementedException();
        }

        internal override void GetCreateIndexSQLString(Table table, Index index, ref List<string> createIndexScript)
        {
            // exist table ve exist index ekle
            string createIndexSqlString = $"ALTER TABLE {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i")}";
            string[] indexColumns = new string[index.IndexColumnList.Values.Count];
            index.IndexColumnList.Values.CopyTo(indexColumns, 0);

            ///CREATE UNIQUE INDEX title_idx ON films (title)
            if (index.IndexType == INDEXTYPE.PRIMARYKEY)
            {
                string dropIndex = createIndexSqlString + $" DROP CONSTRAINT  IF EXISTS  {table.TableName.ToLower().Replace("ı", "i")}_pkey";
                createIndexScript.Add(dropIndex);
                createIndexScript.Add($"{createIndexSqlString} ADD PRIMARY KEY ({string.Join(",", indexColumns).ToLower().Replace("ı", "i")})");


            }
            else
            {
                createIndexSqlString = $@" CREATE INDEX  IF NOT EXISTS {index.IndexName.ToLower().Replace("ı", "i")}_{table.SchemaName.ToLower().Replace("ı", "i")}_{table.TableName.ToLower().Replace("ı", "i")}
                                            ON {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i")} 
                    ({string.Join(",", indexColumns).ToLower().Replace("ı", "i")})";
                createIndexScript.Add(createIndexSqlString);
            }
        }




        internal override string GetCurrentIdentityValueSQLString(Table table, TableColumn tableColumn)
        {
            string getcurrentSqlString = $@"Select max({tableColumn.ColumnName.ToLower().Replace("ı", "i")}) 
                                            from {table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i")}";
            return getcurrentSqlString;

        }

        internal override string SetIdentitySQLString(Table table, TableColumn tableColumn, int identityCurrentValue)
        {
            string serialname = GetSerialName(table, tableColumn);


            string sql = $@"SELECT setval('{serialname}', {identityCurrentValue}, true)";
            return sql;
        }

        private string GetSerialName(Table table, TableColumn tableColumn)
        {
            string sql = $@"select * from pg_get_serial_sequence('{table.SchemaName.ToLower().Replace("ı", "i")}.{table.TableName.ToLower().Replace("ı", "i")}', '{tableColumn.ColumnName.ToLower().Replace("ı", "i")}')";
            DataTable tbl = GetData(sql);
            return tbl.Rows[0][0].ToString();

        }
    }




}
