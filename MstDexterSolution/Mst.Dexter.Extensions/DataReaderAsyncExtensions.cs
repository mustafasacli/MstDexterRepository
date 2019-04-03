namespace Mst.Dexter.Extensions
{
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Threading.Tasks;

    public static class DataReaderAsyncExtensions
    {
        #region [ FirstRowAsync method ]

        public static Task<ExpandoObject> FirstRowAsync(this IDataReader dataReader)
        {
            Task<ExpandoObject> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.FirstRow(dataReader);
            });

            return resultTask;
        }

        #endregion

        #region [ LastRowAsync method ]

        public static Task<ExpandoObject> LastRowAsync(this IDataReader dataReader)
        {
            Task<ExpandoObject> resultTask = Task.Factory.StartNew(() =>
            {
                return DataReaderExtensions.LastRow(dataReader);
            });

            return resultTask;
        }

        #endregion

        #region [ GetDynamicResultSetAsync method ]

        /// <summary>
        /// Bind IDataReader content to ExpandoObject list.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="closeAtFinal"></param>
        /// <returns>Returns ExpandoObject object list.</returns>
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