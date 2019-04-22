namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A data reader extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DataReaderExtensions
    {
        #region [ CloseIfNot method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDataReader extension method that closes if not. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dataReader">   The dataReader to act on. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static void CloseIfNot(this IDataReader dataReader)
        {
            if (dataReader != null && !dataReader.IsClosed)
                dataReader.Close();
        }

        #endregion [ CloseIfNot method ]

        #region [ FirstRow method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDataReader extension method that first row. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="dataReader">   The dataReader to act on. </param>
        ///
        /// <returns>   An ExpandoObject. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static ExpandoObject FirstRow(this IDataReader dataReader)
        {
            if (dataReader == null)
                throw new ArgumentNullException(nameof(dataReader));

            ExpandoObject d = new ExpandoObject();

            if (dataReader.IsClosed)
            {
                return d;
            }

            try
            {
                while (dataReader.Read())
                {
                    d = GetDynamicFromDataReader(dataReader);
                    break;
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }

            return d;
        }

        #endregion [ FirstRow method ]

        #region [ LastRow method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDataReader extension method that last row. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="dataReader">   The dataReader to act on. </param>
        ///
        /// <returns>   An ExpandoObject. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static ExpandoObject LastRow(this IDataReader dataReader)
        {
            if (dataReader == null)
                throw new ArgumentNullException(nameof(dataReader));

            ExpandoObject d = new ExpandoObject();

            if (dataReader.IsClosed)
            {
                return d;
            }

            try
            {
                while (dataReader.Read())
                {
                    d = GetDynamicFromDataReader(dataReader);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (dataReader != null && !dataReader.IsClosed)
                    dataReader.Close();
            }

            return d;
        }

        #endregion [ LastRow method ]

        #region [ GetDynamicResultSet method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Bind IDataReader content to ExpandoObject list. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="reader">       . </param>
        /// <param name="closeAtFinal"> (Optional) </param>
        ///
        /// <returns>   Returns ExpandoObject object list. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<ExpandoObject> GetDynamicResultSet(
            this IDataReader reader, bool closeAtFinal = false)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            List<ExpandoObject> list = new List<ExpandoObject>();

            if (reader.IsClosed)
            {
                return list;
            }

            try
            {
                while (reader.Read())
                {
                    ExpandoObject d = GetDynamicFromDataReader(reader);
                    list.Add(d);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reader != null && closeAtFinal)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetDynamicResultSet method ]

        #region [ GetDynamicResultSetWithPaging method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDataReader extension method that gets dynamic result set with paging.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="reader">           . </param>
        /// <param name="pageNumber">       (Optional) The page number. </param>
        /// <param name="pageItemCount">    (Optional) Number of page ıtems. </param>
        /// <param name="closeAtFinal">     (Optional) </param>
        ///
        /// <returns>   The dynamic result set with paging. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<ExpandoObject> GetDynamicResultSetWithPaging(this IDataReader reader,
            uint pageNumber = 1, uint pageItemCount = 10, bool closeAtFinal = false)
        {
            if (reader == null)
                throw new ArgumentNullException(nameof(reader));

            List<ExpandoObject> list = new List<ExpandoObject>();

            if (reader.IsClosed)
            {
                return list;
            }

            try
            {
                uint cntr = 1;
                uint max = pageNumber * pageItemCount;
                uint min = (pageNumber - 1) * pageItemCount;

                while (reader.Read())
                {
                    if (cntr <= min)
                        continue;

                    if (cntr > max)
                        break;

                    cntr++;

                    ExpandoObject d = GetDynamicFromDataReader(reader);
                    list.Add(d);
                }
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (reader != null && closeAtFinal)
                    reader.Close();
            }

            return list;
        }

        #endregion [ GetDynamicResultSetWithPaging method ]

        #region [ GetMultipleDynamicResultSet method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDataReader extension method that gets multi dynamic result set. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="reader">       . </param>
        /// <param name="closeAtFinal"> (Optional) </param>
        ///
        /// <returns>   The multi dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<List<ExpandoObject>> GetMultiDynamicResultSet(
            this IDataReader reader, bool closeAtFinal = false)
        {
            List<List<ExpandoObject>> objDynList = new List<List<ExpandoObject>>();

            try
            {
                List<ExpandoObject> resultSet = null;

                do
                {
                    resultSet = new List<ExpandoObject>();
                    resultSet = reader.GetDynamicResultSet();
                    objDynList.Add(resultSet);
                } while (reader.NextResult());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (reader != null && closeAtFinal)
                    reader.Close();
            }

            return objDynList;
        }

        #endregion [ GetMultipleDynamicResultSet method ]

        #region [ GetDynamicFromDataReader method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets dynamic from data reader. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dataReader">   The dataReader to act on. </param>
        ///
        /// <returns>   The dynamic from data reader. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        internal static ExpandoObject GetDynamicFromDataReader(IDataReader dataReader)
        {
            IDictionary<string, object> expando = new ExpandoObject();
            var fieldCount = dataReader.FieldCount;
            var columnName = string.Empty;
            object value;
            var colCounter = 1;

            for (int counter = 0; counter < fieldCount; counter++)
            {
                columnName = dataReader.GetName(counter);
                value = dataReader.GetValue(counter);
                value = value == DBNull.Value ? null : value;

                if (expando.ContainsKey(columnName))
                {
                    colCounter = 1;
                    while (expando.ContainsKey(columnName))
                    {
                        columnName = $"{columnName}_{colCounter}";
                        colCounter++;
                    }
                }

                expando[columnName] = value;
            }

            ExpandoObject d = expando as ExpandoObject;
            return d;
        }

        #endregion [ GetDynamicFromDataReader method ]
    }
}