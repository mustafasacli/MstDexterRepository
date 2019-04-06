namespace Mst.Dexter.PocoGenerator.Source.Util
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    internal class DbDataUtil
    {
        #region [ Copy method ]

        public static DataTable Copy(DataTable dt)
        {
            try
            {
                DataTable _dataT = new DataTable();
                foreach (DataColumn col in dt.Columns)
                {
                    _dataT.Columns.Add(col.ColumnName, col.DataType);
                }

                DataRow dr;
                foreach (DataRow row in dt.Rows)
                {
                    dr = null;
                    dr = _dataT.NewRow();
                    foreach (DataColumn col in dt.Columns)
                    {
                        dr[col.ColumnName] = row[col.ColumnName];
                    }
                    _dataT.Rows.Add(dr);
                }

                return _dataT;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion [ Copy method ]

        #region [ GetSomeColumnsAsTable method ]

        public static DataTable GetSomeColumnsAsTable(DataTable dt, string[] columnList)
        {
            try
            {
                DataTable dtNew = new DataTable();
                if (dt == null)
                    return dtNew;

                if (columnList == null)
                    return dtNew;

                if (columnList.Length == 0)
                    return dtNew;

                DataColumn _col;
                foreach (string col in columnList)
                {
                    _col = dt.Columns[col];
                    dtNew.Columns.Add(_col.ColumnName, _col.DataType);
                }

                DataRow dr = null;
                foreach (DataRow row in dt.Rows)
                {
                    dr = dtNew.NewRow();
                    foreach (string col in columnList)
                    {
                        dr[col] = row[col];
                    }
                    dtNew.Rows.Add(dr);
                }
                return dtNew;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        #endregion [ GetSomeColumnsAsTable method ]

        #region [ GetColumnAsUniqueList method ]

        public static List<string> GetColumnAsUniqueList(DataTable dt, string column)
        {
            List<string> cols = new List<string>();
            try
            {
                string strCol;
                foreach (DataRow row in dt.Rows)
                {
                    strCol = string.Format("{0}", row[column]);
                    if (cols.Contains(strCol) == false)
                        cols.Add(strCol);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            return cols;
        }

        #endregion [ GetColumnAsUniqueList method ]
    }
}