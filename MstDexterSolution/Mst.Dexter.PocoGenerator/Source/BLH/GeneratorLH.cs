using Mst.Dexter.PocoGenerator.Source.BO;
using Mst.Dexter.PocoGenerator.Source.DL;
using Mst.Dexter.PocoGenerator.Source.Enum;
using Mst.Dexter.PocoGenerator.Source.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Mst.Dexter.PocoGenerator.Source.BLH
{
    internal class GeneratorLH
    {
        private GeneratorDL generatorDL;

        #region [ GeneratorLH Ctor ]

        public GeneratorLH()
        {
            generatorDL = new GeneratorDL();
        }

        #endregion [ GeneratorLH Ctor ]

        #region [ GetTablesAndColumns method ]

        public List<Table> GetTablesAndColumns(RcConnectionTypes connectionType, string connectionString)
        {
            List<Table> tables = new List<Table>();

            try
            {
                switch (connectionType)
                {
                    default:
                        break;

                    case RcConnectionTypes.Sql:
                        tables = GetSqlServerTables(connectionString);
                        break;

                    case RcConnectionTypes.PostgreSql:
                        tables = GetPostgreSQLTables(connectionString);
                        break;

                    case RcConnectionTypes.DB2:
                        GetDB2Tables(connectionString);
                        break;

                    case RcConnectionTypes.Oracle:
                        tables = GetOracleTablesM(connectionString);
                        break;

                    case RcConnectionTypes.Oledb:
                        tables = GetOledbTable(connectionString);
                        break;

                    case RcConnectionTypes.Odbc:
                        tables = GetOdbcTable(connectionString);
                        break;
                        /*
                    case RcConnectionTypes.MySql:
                        tables = GetMySQLAndMariaDbTables(connectionString);
                        break;

                    case RcConnectionTypes.OracleNet:
                        tables = GetOracleTablesN(connectionString);
                        break;

                    case RcConnectionTypes.SqlServerCe:
                        tables = GetSqlServerCeTables(connectionString);
                        break;

                    case RcConnectionTypes.VistaDB:
                        tables = GetVistaDBTables(connectionString);
                        break;

                    case RcConnectionTypes.SQLite:
                        tables = GetSqliteTables(connectionString);
                        break;

                    case RcConnectionTypes.FireBird:
                        tables = GetFireBirdTables(connectionString);
                        break;

                    case RcConnectionTypes.Sybase:
                        tables = GetSybaseTables(connectionString);
                        break;

                    case RcConnectionTypes.Informix:
                        tables = GetInformixTables(connectionString);
                        break;

               case RcConnectionTypes.U2:
                   break;

                   case RcConnectionTypes.Synergy:
                       break;

                   case RcConnectionTypes.Ingres:
                       break;

                    case RcConnectionTypes.SqlBase:
                        tables = GetSqlBaseTables(connectionString);
                        break;
                    */
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return tables;
        }

        #endregion [ GetTablesAndColumns method ]

        #region [ GetSqlServerTables method ]

        private List<Table> GetSqlServerTables(string connectionString)
        {
            List<Table> tableList = new List<Table>();

            try
            {
                DataTable dt = new DataTable();
                dt = generatorDL.GetSqlServerTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "TABLE_NAME");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;

                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        //columns.Add(new Column(rw["COLUMN_NAME"].ToString(), rw["DATA_TYPE"].ToString()));
                        
                        // TODO
                        Column cl = null;
                        //rw.RowToObject<Column>(dt.Columns);
                        columns.Add(cl);
                        if (cl.IdentityState == 1 && string.IsNullOrWhiteSpace(tbl.IdColumn))
                            tbl.IdColumn = cl.ColumnName;
                    }

                    tbl.TableColumns = columns;
                    /*
                    rows = null;
                    rows = dt.Select(string.Format("TABLE_NAME='{0}' AND IdentityState=1", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["COLUMN_NAME"].ToString();
                        break;
                    }
                    */
                    tableList.Add(tbl);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return tableList;
        }

        #endregion [ GetSqlServerTables method ]

        #region [ GetPostgreSQLTables method ]

        private List<Table> GetPostgreSQLTables(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataTable dt = new DataTable();
                dt = generatorDL.GetPostgreSQL(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "table_name");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("table_name='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column { ColumnName = rw["column_name"].ToString(), DataType = rw["udt_name"].ToString() });
                    }

                    tbl.TableColumns = columns;
                    /*
                     * Identity Column bulunacak.
                    */
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND udt_name LIKE '%int%' AND column_default LIKE '%nextval%'", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["column_name"].ToString();
                        break;
                    }
                    tableList.Add(tbl);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return tableList;
        }

        #endregion [ GetPostgreSQLTables method ]

        #region [ GetOracleTablesM method ]

        private List<Table> GetOracleTablesM(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetOracleTables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "TABLE_NAME");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                Column col;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        col = new Column
                        {
                            ColumnName = string.Format("{0}", row["COLUMN_NAME"]),
                            DataType = string.Format("{0}", row["DATA_TYPE"]),
                            IsRequired = string.Format("{0}", row["NULLABLE"]).Trim() == "N" ? 1 : 0
                        };

                        cols.Add(col);
                    }
                    _table.TableColumns = cols;
                    /*
                    rows = null;
                    rows = dt.Select(string.Format("table_name='{0}' AND coltype IN(6, 18, 53)", tbl));

                    foreach (DataRow row in rows)
                    {
                        _table.IdColumn = string.Format("{0}", row["colname"]);
                        break;
                    }

                     */
                    _tables.Add(_table);
                    rows = null;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return _tables;
        }

        #endregion [ GetOracleTablesM method ]

        #region [ GetOledbTable method ]

        private List<Table> GetOledbTable(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataSet ds = new DataSet();
                DataRow[] rows;
                List<string> tables = new List<string>();
                ds = generatorDL.GetOledbSet(connectionString);
                rows = ds.Tables[0].Select("TABLE_TYPE='TABLE'");
                string str_tbl;
                foreach (DataRow rw in rows)
                {
                    str_tbl = string.Format("{0}", rw["TABLE_NAME"]);

                    if (tables.Contains(str_tbl) == false)
                        tables.Add(str_tbl);
                }

                List<Column> columns;
                Table tbl;
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = ds.Tables[1].Select(string.Format("TABLE_NAME='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column
                        {
                            ColumnName = rw["COLUMN_NAME"].ToString(),
                            DataType = OleDbUtil.GetIntFrom(ConvertUtil.ToInt(rw["DATA_TYPE"].ToString()))
                        });
                    }

                    tbl.TableColumns = columns;
                    /*
                     * Identity Column bulunacak.
                    */
                    rows = null;
                    rows = ds.Tables[1].Select(string.Format("TABLE_NAME='{0}' AND DATA_TYPE=3", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["COLUMN_NAME"].ToString();
                        break;
                    }
                    tableList.Add(tbl);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return tableList;
        }

        #endregion [ GetOledbTable method ]

        #region [ GetOdbcTable method ]

        private List<Table> GetOdbcTable(string connectionString)
        {
            List<Table> tableList = new List<Table>();
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds = generatorDL.GetOdbcTable(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(ds.Tables[0], "TABLE_NAME");
                DataRow[] rows;
                List<Column> columns;
                Table tbl;
                dt = ds.Tables[1];
                foreach (string strTable in tables)
                {
                    tbl = new Table()
                    {
                        TableName = strTable
                    };
                    columns = new List<Column>();
                    rows = dt.Select(string.Format("TABLE_NAME='{0}'", strTable));
                    foreach (DataRow rw in rows)
                    {
                        columns.Add(new Column
                        {
                            ColumnName = rw["COLUMN_NAME"].ToString(),
                            DataType = rw["TYPE_NAME"].ToString()
                        });
                    }

                    tbl.TableColumns = columns;
                    /*
                     * Identity Column bulunacak.
                    */
                    rows = null;
                    rows = dt.Select(string.Format("TABLE_NAME='{0}' AND TYPE_NAME='int identity'", strTable));

                    foreach (DataRow rw2 in rows)
                    {
                        tbl.IdColumn = rw2["COLUMN_NAME"].ToString();
                    }
                    tableList.Add(tbl);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return tableList;
        }

        #endregion [ GetOdbcTable method ]

        #region [ GetDB2Tables method ]

        private List<Table> GetDB2Tables(string connectionString)
        {
            List<Table> _tables = new List<Table>();
            try
            {
                DataTable dt = generatorDL.GetDB2Tables(connectionString);
                List<string> tables = DbDataUtil.GetColumnAsUniqueList(dt, "tbname");
                Table _table;
                DataRow[] rows;
                List<Column> cols;
                foreach (string tbl in tables)
                {
                    _table = new Table()
                    {
                        TableName = tbl
                    };
                    cols = new List<Column>();
                    rows = dt.Select(string.Format("tbname='{0}'", tbl));
                    foreach (DataRow row in rows)
                    {
                        cols.Add(new Column { ColumnName = string.Format("{0}", row["name"]), DataType = string.Format("{0}", row["coltype"]) });
                    }
                    _table.TableColumns = cols;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return _tables;
        }

        #endregion [ GetDB2Tables method ]
    }
}