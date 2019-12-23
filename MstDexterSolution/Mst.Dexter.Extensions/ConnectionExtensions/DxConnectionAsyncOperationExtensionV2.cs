namespace Mst.Dexter.Extensions
{
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Threading.Tasks;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A Dx connection asynchronous operation extension. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DxConnectionAsyncOperationExtensionV2
    {
        #region [ ExecuteAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that executes the asynchronous operation.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the execute. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Task<int> ExecuteAsync(this IDbConnection connection,
            string sql,
            CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.Execute(connection,
            sql,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ ExecuteAsync method ]

        #region [ ExecuteReaderAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that executes the reader asynchronous operation.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the execute reader. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Task<IDataReader> ExecuteReaderAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.ExecuteReader(connection,
            sql,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ ExecuteReaderAsync method ]

        #region [ ExecuteScalarAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that executes the scalar asynchronous operation.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the execute scalar. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Task<object> ExecuteScalarAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.ExecuteScalar(connection,
            sql,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ ExecuteScalarAsync method ]

        #region [ GetResultSetAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets result set asynchronous. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Task<DataSet> GetResultSetAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.GetResultSet(connection,
            sql,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ GetResultSetAsync method ]

        #region [ GetDynamicResultSetAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that gets dynamic result set asynchronous.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Task<List<ExpandoObject>> GetDynamicResultSetAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.GetDynamicResultSet(connection,
            sql,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetAsync method ]

        #region [ GetDynamicResultSetWithPagingAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that gets dynamic result set with paging asynchronous.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="pageNumber">       (Optional) The page number. </param>
        /// <param name="pageItemCount">    (Optional) Number of page ıtems. </param>
        /// <param name="parameters">       A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the dynamic result set with paging. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////

        public static Task<List<ExpandoObject>> GetDynamicResultSetWithPagingAsync(this IDbConnection connection,
            string sql, CommandType commandType,
            IDbTransaction transaction = null,
            uint pageNumber = 1, uint pageItemCount = 10,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.GetDynamicResultSetWithPaging(connection,
            sql,
            commandType,
            transaction,
            pageNumber: pageNumber,
            pageItemCount: pageItemCount,
            parameters: parameters);
            });

            return resultTask;
        }

        #endregion [ GetDynamicResultSetWithPagingAsync method ]

        #region [ GetMultiDynamicResultSetAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that gets multi dynamic result set asynchronous.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sql">          The SQL. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the multi dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<List<List<ExpandoObject>>> GetMultiDynamicResultSetAsync(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.GetMultiDynamicResultSet(connection,
            sql,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ GetMultiDynamicResultSetAsync method ]

        #region [ FirstAsDynamicAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that first as dynamic asynchronous. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the first as dynamic. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<ExpandoObject> FirstAsDynamicAsync(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.FirstAsDynamic(connection,
            sqlText,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ FirstAsDynamicAsync method ]

        #region [ FirstAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that first asynchronous. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields a T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<T> FirstAsync<T>(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           params object[] parameters) where T : class
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.First<T>(connection,
            sqlText,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ FirstAsync method ]

        #region [ LastAsDynamicAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that last as dynamic asynchronous. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields the last as dynamic. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<ExpandoObject> LastAsDynamicAsync(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
           params object[] parameters)
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.LastAsDynamic(connection,
            sqlText,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ LastAsDynamicAsync method ]

        #region [ LastAsync method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that last asynchronous. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">   The connection to act on. </param>
        /// <param name="sqlText">      The SQL text. </param>
        /// <param name="commandType">  (Optional) Type of the command. </param>
        /// <param name="transaction">  (Optional) The transaction. </param>
        /// <param name="parameters">   A variable-length parameters list containing parameters. </param>
        ///
        /// <returns>   An asynchronous result that yields a T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static Task<T> LastAsync<T>(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           params object[] parameters) where T : class
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.Last<T>(connection,
            sqlText,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion [ LastAsync method ]

        #region [ GetListAsync method ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static Task<List<T>> GetListAsync<T>(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           params object[] parameters) where T : class
        {
            var resultTask = Task.Factory.StartNew(() =>
            {
                return DxConnectionOperationExtensionV2.GetList<T>(connection,
            sqlText,
            commandType,
            transaction,
            parameters);
            });

            return resultTask;
        }

        #endregion
    }
}