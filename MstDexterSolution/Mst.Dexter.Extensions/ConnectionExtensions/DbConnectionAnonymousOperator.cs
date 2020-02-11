namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    /// <summary>
    /// 
    /// </summary>
    public static class DbConnectionAnonymousOperator
    {
        /// <summary>
        /// Gets parameters from 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParametersFromObject(this IDbConnection connection, object obj)
        {
            var properties = new Dictionary<string, object>();
            if (obj == null)
                return properties;

            var prefix = connection.GetParameterPrefix();
            var isOdbc = false;
            {
                var connectionType = connection.GetDbConnectionType();
                isOdbc = connectionType.IsOdbcConn();
            }

            obj.GetType()
                .GetProperties()
                .ToList()
                .ForEach(q =>
                {
                    properties[string.Format("{0}{1}", isOdbc ? string.Empty : prefix, q.Name)] = q.GetValue(obj, null);
                });

            return properties;
        }

        #region [ Execute method ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int QueryExec(this IDbConnection connection,
           string sql, object obj,
           CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null)
        {
            int res = 0;

            try
            {
                var inputs = GetParametersFromObject(connection, obj);
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    var outputs = new Dictionary<string, object>();
                    DxDbCommandHelper.SetCommandParameters(command, inputs, outputs);

                    res = command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion

        #region [ ExecuteScalar method ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="obj"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static object QueryScalar(this IDbConnection connection,
            string sql, object obj,
            CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null)
        {
            object res = null;

            try
            {
                var inputs = GetParametersFromObject(connection, obj);
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    var outputs = new Dictionary<string, object>();
                    DxDbCommandHelper.SetCommandParameters(command, inputs, outputs);

                    res = command.ExecuteScalar();
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion

        #region [ First method ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="obj"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static T QueryFirst<T>(this IDbConnection connection,
            string sqlText, object obj, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null) where T : class, new()
        {
            T instance = null;

            try
            {
                var inputs = GetParametersFromObject(connection, obj);
                var outputs = new Dictionary<string, object>();
                ExpandoObject expando = connection.FirstAsDynamic(
                    sqlText, commandType,
                    transaction, inputs, outputs);

                instance = expando.ConvertTo<T>();
            }
            catch (Exception e)
            {
                throw;
            }

            return instance;
        }

        #endregion

        #region [ Last method ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="obj"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static T QueryLast<T>(this IDbConnection connection,
            string sqlText, object obj, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null) where T : class, new()
        {
            T instance = null;

            try
            {
                var inputs = GetParametersFromObject(connection, obj);
                var outputs = new Dictionary<string, object>();
                ExpandoObject expando = connection.LastAsDynamic(
                    sqlText, commandType,
                    transaction, inputs, outputs);

                instance = expando.ConvertTo<T>();
            }
            catch (Exception e)
            {
                throw;
            }

            return instance;
        }

        #endregion

        #region [ GetList method ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="obj"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static List<T> QueryList<T>(this IDbConnection connection,
            string sqlText, object obj, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null) where T : class, new()
        {
            var inputs = GetParametersFromObject(connection, obj);
            var outputs = new Dictionary<string, object>();
            var dynList = connection.GetDynamicResultSet(sqlText, commandType, transaction, inputs, outputs);
            var resultSet = DynamicExtensions.ConvertToList<T>(dynList);
            return resultSet;
        }

        #endregion

        #region [ GetListSkipAndTake method ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="obj"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static List<T> QueryListSkipAndTake<T>(this IDbConnection connection,
            string sqlText, object obj, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            uint skip = 0, uint take = 0) where T : class, new()
        {
            var inputs = GetParametersFromObject(connection, obj);
            var outputs = new Dictionary<string, object>();
            var dynList = connection.GetDynamicResultSetSkipAndTake(sqlText, commandType, transaction, inputs, outputs, skip: skip, take: take);
            var resultSet = DynamicExtensions.ConvertToList<T>(dynList);
            return resultSet;
        }

        #endregion
    }
}
