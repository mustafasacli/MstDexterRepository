namespace Mst.Dexter.Extensions
{
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Threading.Tasks;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A data reader asynchronous extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DataReaderAsyncExtensions
    {
        #region [ FirstRowAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDataReader extension method that first row asynchronous. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dataReader">   The dataReader to act on. </param>
        ///
        /// <returns>   An asynchronous result that yields the first row. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<ExpandoObject> FirstRowAsync(this IDataReader dataReader)
        {
            Task<ExpandoObject> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.FirstRow(dataReader);
            });

            return resultTask;
        }

        #endregion [ FirstRowAsync method ]

        #region [ LastRowAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDataReader extension method that last row asynchronous. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dataReader">   The dataReader to act on. </param>
        ///
        /// <returns>   An asynchronous result that yields the last row. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<ExpandoObject> LastRowAsync(this IDataReader dataReader)
        {
            Task<ExpandoObject> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.LastRow(dataReader);
            });

            return resultTask;
        }

        #endregion [ LastRowAsync method ]

        #region [ GetDynamicResultSetAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Bind IDataReader content to ExpandoObject list. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="reader">       . </param>
        /// <param name="closeAtFinal"> (Optional) </param>
        ///
        /// <returns>   Returns ExpandoObject object list. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<List<ExpandoObject>> GetDynamicResultSetAsync(
            this IDataReader reader, bool closeAtFinal = false)
        {
            Task<List<ExpandoObject>> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.GetDynamicResultSet(reader, closeAtFinal);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetAsync method ]

        #region [ GetDynamicResultSetWithPagingAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDataReader extension method that gets dynamic result set with paging asynchronous.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="reader">           . </param>
        /// <param name="pageNumber">       (Optional) The page number. </param>
        /// <param name="pageItemCount">    (Optional) Number of page ıtems. </param>
        /// <param name="closeAtFinal">     (Optional) </param>
        ///
        /// <returns>   An asynchronous result that yields the dynamic result set with paging. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<List<ExpandoObject>> GetDynamicResultSetWithPagingAsync(this IDataReader reader,
            uint pageNumber = 1, uint pageItemCount = 10, bool closeAtFinal = false)
        {
            Task<List<ExpandoObject>> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.GetDynamicResultSetWithPaging(reader, pageNumber, pageItemCount, closeAtFinal);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetWithPagingAsync method ]

        #region [ GetMultiDynamicResultSetAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDataReader extension method that gets multi dynamic result set asynchronous.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="reader">   . </param>
        ///
        /// <returns>   An asynchronous result that yields the multi dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<List<List<ExpandoObject>>> GetMultiDynamicResultSetAsync(
           this IDataReader reader)
        {
            Task<List<List<ExpandoObject>>> resultTask = Task.Factory.StartNew(() =>
        {
            return DataReaderExtensions.GetMultiDynamicResultSet(reader);
        });

            return resultTask;
        }

        #endregion [ GetMultiDynamicResultSetAsync method ]
    }
}