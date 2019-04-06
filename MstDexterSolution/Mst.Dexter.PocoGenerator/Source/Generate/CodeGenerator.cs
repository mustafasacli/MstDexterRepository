namespace Mst.Dexter.PocoGenerator.Source.Generate
{
    using Mst.Dexter.PocoGenerator.Source.BLH;
    using Mst.Dexter.PocoGenerator.Source.BO;
    using Mst.Dexter.PocoGenerator.Source.Enum;
    using System;
    using System.Collections.Generic;

    internal class CodeGenerator
    {
        public static string ConnStr = string.Empty;
        public static int Index = -1;

        private string _ConnectionString = string.Empty;

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        internal RcConnectionTypes ConnType { get; set; } = RcConnectionTypes.Sql;

        public List<Table> GetTablesAndColumns(RcConnectionTypes connectionType, string connStr)
        {
            try
            {
                GeneratorLH genLH = new GeneratorLH();
                return genLH.GetTablesAndColumns(connectionType, connStr);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /*
        public List<Table> GetTableList()
        {
            try
            {
                FreeConnection frCon = FreeConnection.GetConnection(Index);
                frCon.ConnectionString = ConnStr;
                string sqlQuery = Crud.GetTablesQuery(frCon.ConnectionType);

                DataTable dT = frCon.GetResultSet(sqlQuery).Tables[0];
                List<Table> classtable = new List<Table>();

                foreach (DataRow row in dT.Rows)
                {
                    classtable.Add(new Table()
                    {
                        TableName = row[0].ToString()
                    });
                }

                return classtable;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public List<Column> GetColumnsOfTable(string tableName)
        {
            try
            {
                FreeConnection frCon = FreeConnection.GetConnection(Index);
                frCon.ConnectionString = ConnStr;
                string sqlQuery = Crud.GetColumnsOfTablesQuery(frCon.ConnectionType);

                DataTable dT = frCon.GetResultSet(string.Format(sqlQuery, tableName)).Tables[0];
                List<Column> columns = new List<Column>();

                foreach (DataRow row in dT.Rows)
                {
                    columns.Add(
                        new Column(
                            row[0].ToString(),  // for Column Name
                            row[1].ToString()   // for Column Datatype
                    ));
                }

                return columns;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public string GetIdColumnOfTable(string tableName)
        {
            try
            {
                FreeConnection frCon = FreeConnection.GetConnection(Index);
                frCon.ConnectionString = ConnStr;
                string query = string.Format(Crud.GetIdColumnOfTable(frCon.ConnectionType), tableName);
                if (string.IsNullOrWhiteSpace(query))
                    return string.Empty;
                DataTable dtIdCols = frCon.GetResultSet(query).Tables[0];
                string IdColumn = "";
                foreach (DataRow row in dtIdCols.Rows)
                {
                    IdColumn = row[0].ToString(); // IdColumn
                    break;
                }
                return IdColumn;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        */
    }
}