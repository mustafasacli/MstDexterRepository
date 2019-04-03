namespace Mst.Dexter.Extensions
{
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Threading.Tasks;

    public static class DxConnectionAsyncOperationExtension
    {
        #region [ ExecuteAsync method ]

        public static Task<int> ExecuteAsync(this IDbConnection connection,
            string sql,
            CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.Execute(connection,
            sql,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ ExecuteAsync method ]

        #region [ ExecuteReaderAsync method ]

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.ExecuteReader(connection,
            sql,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ ExecuteReaderAsync method ]

        #region [ ExecuteScalarAsync method ]

        public static Task<object> ExecuteScalarAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.ExecuteScalar(connection,
            sql,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ ExecuteScalarAsync method ]

        #region [ GetResultSetAsync method ]

        public static Task<DataSet> GetResultSetAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.GetResultSet(connection,
            sql,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ GetResultSetAsync method ]

        #region [ GetDynamicResultSetAsync method ]

        public static Task<List<ExpandoObject>> GetDynamicResultSetAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.GetDynamicResultSet(connection,
            sql,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetAsync method ]

        #region [ GetDynamicResultSetWithPagingAsync method ]

        public static Task<List<ExpandoObject>> GetDynamicResultSetWithPagingAsync(this IDbConnection connection,
            string sql, CommandType commandType,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null,
            uint pageNumber = 1, uint pageItemCount = 10)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.GetDynamicResultSetWithPaging(connection,
            sql,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetWithPagingAsync method ]

        #region [ GetMultiDynamicResultSetAsync method ]

        public static Task<List<List<ExpandoObject>>> GetMultiDynamicResultSetAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.GetMultiDynamicResultSet(connection,
            sql,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ GetMultiDynamicResultSetAsync method ]

        #region [ FirstAsDynamicAsync method ]

        public static Task<ExpandoObject> FirstAsDynamicAsync(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.FirstAsDynamic(connection,
            sqlText,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ FirstAsDynamicAsync method ]

        #region [ FirstAsync method ]

        public static Task<T> FirstAsync<T>(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null) where T : class
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.First<T>(connection,
            sqlText,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ FirstAsync method ]

        #region [ LastAsDynamicAsync method ]

        public static Task<ExpandoObject> LastAsDynamicAsync(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.LastAsDynamic(connection,
            sqlText,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ LastAsDynamicAsync method ]

        #region [ LastAsync method ]

        public static Task<T> LastAsync<T>(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null) where T : class
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtension.Last<T>(connection,
            sqlText,
            commandType,
            transaction,
             inputParameters,
             outputParameters);
            });

            return resultTask;
        }

        #endregion [ LastAsync method ]
    }
}