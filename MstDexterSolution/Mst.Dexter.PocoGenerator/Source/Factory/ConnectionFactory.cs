namespace Mst.Dexter.PocoGenerator.Source.Factory
{
    using Mst.Dexter.Extensions;
    using Mst.Dexter.PocoGenerator.Source.QO;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;

    public sealed class ConnectionFactory
    {
        public static List<ExpandoObject> GetConnectionTables(IDbConnection dbConn)
        {
            var list = new List<ExpandoObject>();

            var dbType = dbConn.GetDbConnectionType();
            var query = Crud.GetTablesQuery(dbType);

            var dbNameFormat = "#DB_NAME#";

            if (query.Contains(dbNameFormat))
            {
                if (!string.IsNullOrWhiteSpace(dbConn.Database))
                {
                    query = query.Replace(dbNameFormat, dbConn.Database.Replace("'", "''"));
                }
                else
                {
                    var connStringBuilder = dbConn.GetConnectionStringBuilder();
                    connStringBuilder.ConnectionString = dbConn.ConnectionString;

                    if (connStringBuilder.ContainsKey("USER ID"))
                    {
                        query = query.Replace(dbNameFormat, connStringBuilder["USER ID"].ToStr());
                    }
                }
            }

            list = GetResultset(dbConn, query) ?? new List<ExpandoObject>();

            return list;
        }

        public static List<ExpandoObject> GetTableColumns(IDbConnection dbConn, string tableName, string schemaName)
        {
            var list = new List<ExpandoObject>();

            var dbType = dbConn.GetDbConnectionType();
            var query = Crud.GetColumnsOfTablesQuery(dbType);

            var dbNameFormat = "#DB_NAME#";
            var tableNameFormat = "#TABLE_NAME#";

            if (query.Contains(tableNameFormat))
                query = query.Replace(tableNameFormat, tableName);

            if (query.Contains(dbNameFormat))
            {
                if (!string.IsNullOrWhiteSpace(dbConn.Database))
                {
                    query = query.Replace(dbNameFormat, dbConn.Database.Replace("'", "''"));
                }
                else
                {
                    var connStringBuilder = dbConn.GetConnectionStringBuilder();
                    connStringBuilder.ConnectionString = dbConn.ConnectionString;

                    if (connStringBuilder.ContainsKey("USER ID"))
                    {
                        query = query.Replace(dbNameFormat, connStringBuilder["USER ID"].ToStr());
                    }
                }
            }

            list = GetResultset(dbConn, query) ?? new List<ExpandoObject>();

            return list;
        }

        internal static List<ExpandoObject> GetResultset(IDbConnection dbConn, string query)
        {
            var list = new List<ExpandoObject> { };

            if (string.IsNullOrWhiteSpace(query))
                return list;

            try
            {
                dbConn.Open();

                list = dbConn.GetDynamicResultSet(query);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                dbConn.Close();
            }

            return list;
        }
    }
}