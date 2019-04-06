namespace Mst.Dexter.PocoGenerator.Source.Factory
{
    using Mst.Dexter.Extensions;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Enum;
    using Mst.Dexter.PocoGenerator.Source.Interfaces;
    using Mst.Dexter.PocoGenerator.Source.QO;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    internal sealed class TableFactory
    {
        internal static List<Table> GetTables(IDbConnection dbConnection)
        {
            var tableLst = new List<Table>();
            var tables = new List<ExpandoObject>();
            var dnConnType = dbConnection.GetDbConnectionType();

            if (dnConnType.IsMember(RcConnectionTypes.Oledb, RcConnectionTypes.Odbc))
            {
                {
                    var dataTable = dbConnection
                         .GetType()
                         .GetMethod("GetSchema", new Type[] { typeof(string), typeof(string[]) })
                         .Invoke(dbConnection, new object[] { "TABLES", new string[] { null, null, null, "Table" } }) as DataTable;

                    var dataView = dataTable.DefaultView;
                    dataView.Sort = "TABLE_NAME Asc";
                    dataTable = dataView.ToTable();
                    tables = dataTable.ToDynamicList();
                }
            }
            else
            {
                tables = ConnectionFactory.GetConnectionTables(dbConnection);
            }

            tableLst = BuildTables(tables, dnConnType);

            return tableLst;
        }

        internal static List<string> GetTableList(IDbConnection dbConnection)
        {
            var tableList = new List<string>();

            var connectionType = dbConnection.GetDbConnectionType();

            if (connectionType.IsMember(RcConnectionTypes.Oledb, RcConnectionTypes.Odbc))
            {
                {
                    var dataTable = dbConnection
                         .GetType()
                         .GetMethod("GetSchema", new Type[] { typeof(string), typeof(string[]) })
                         .Invoke(dbConnection, new object[] { "TABLES", new string[] { null, null, null, "Table" } }) as DataTable;

                    var dataView = dataTable.DefaultView;
                    dataView.Sort = "TABLE_NAME Asc";
                    dataTable = dataView.ToTable();
                    tableList = dataTable.GetColumnAsUniqueList<string>("TABLE_NAME");
                }
            }
            else
            {
                var query = Crud.GetTablesAndColumns(connectionType);

                var dbNameFormat = "#DB_NAME#";

                if (query.Contains(dbNameFormat))
                    query = query.Replace(dbNameFormat, dbConnection.Database.Replace("'", "''"));

                var dynamicTables = dbConnection.GetDynamicResultSet(query);

                tableList = dynamicTables
                    .Select(q => (q as IDictionary<string, object>)["TABLE_NAME"].ToStr())
                    .Where(q => string.IsNullOrWhiteSpace(q) == false)
                    .ToList() ?? new List<string>();
            }

            return tableList ?? new List<string> { };
        }

        internal static List<string> GetTableKeyColumns(IDbConnection dbConnection, string tableName)
        {
            var columnList = new List<string>();

            var connectionType = dbConnection.GetDbConnectionType();

            var query = Crud.GetIdColumnOfTable(connectionType);

            if (query.IsNullOrSpace())
                return columnList;

            var tableNameFormat = "#TABLE_NAME#";

            if (query.Contains(tableNameFormat))
                query = query.Replace(tableNameFormat, (tableName ?? string.Empty).Replace("'", "''"));

            var dynamicTables = ConnectionFactory.GetResultset(dbConnection, query);

            columnList = dynamicTables
                .Select(q => (q as IDictionary<string, object>)["COLUMN_NAME"].ToStr())
                .Where(q => string.IsNullOrWhiteSpace(q) == false)
                .ToList() ?? new List<string>();

            return columnList;
        }

        public static List<Column> BuildColumnList(IDbConnection connection, string tableName, string schemaName)
        {
            var columns = ColumnList(connection, tableName, schemaName);

            var columnlist = BuildColumns(columns, connection.GetDbConnectionType());

            return columnlist;
        }

        public static List<ExpandoObject> ColumnList(IDbConnection connection, string tableName, string schemaName)
        {
            var columnList = new List<ExpandoObject>();

            var dnConnType = connection.GetDbConnectionType();

            if (dnConnType.IsMember(RcConnectionTypes.Oledb, RcConnectionTypes.Odbc))
            {
                {
                    var dt = connection
                         .GetType()
                         .GetMethod("GetSchema", new Type[] { typeof(string) })
                         .Invoke(connection, new object[] { "COLUMNS" }) as DataTable;

                    var rows = dt.Select($" TABLE_NAME = {tableName}");
                    dt = rows.CopyToDataTable();

                    var dV = dt.DefaultView;
                    dV.Sort = "ORDINAL_POSITION Asc";
                    dt = dV.ToTable();

                    columnList = dt.ToDynamicList();
                }
            }
            else
            {
                columnList = ConnectionFactory.GetTableColumns(connection, tableName, schemaName);
            }

            return columnList;
        }

        private static List<Table> BuildTables(List<ExpandoObject> tables, RcConnectionTypes dbConnType)
        {
            ITableBuilder tableBuilder = null;

            tableBuilder = TableBuilderFactory.CreateTableBuilder(dbConnType);

            var result = tableBuilder?.CreateTableList(tables);

            return result;
        }

        private static List<Column> BuildColumns(List<ExpandoObject> columns, RcConnectionTypes dbConnType)
        {
            ITableBuilder tableBuilder = null;

            tableBuilder = TableBuilderFactory.CreateTableBuilder(dbConnType);

            var result = tableBuilder?.BuildColumns(columns) ?? new List<Column> { };

            return result;
        }
    }
}