namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dx connection operation extension. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DxConnectionOperationExtension
    {
        #region [ Execute method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   An int. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int Execute(this IDbConnection connection,
            string sql,
            CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            int res = 0;

            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                    res = command.ExecuteNonQuery();

                    if (outputParameters != null && outputParameters.Count > 0)
                        outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion [ Execute method ]

        #region [ ExecuteReader method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes the reader operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        /// <param name="commandBehavior">CommandBehaviour for DataReader</param>
        ///
        /// <returns>   An IDataReader. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static IDataReader ExecuteReader(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null, CommandBehavior? commandBehavior = null)
        {
            IDataReader dataReader = null;

            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                    if (!commandBehavior.HasValue)
                        dataReader = command.ExecuteReader();
                    else
                        dataReader = command.ExecuteReader(commandBehavior.Value);

                    if (outputParameters != null && outputParameters.Count > 0)
                        outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return dataReader;
        }

        #endregion [ ExecuteReader method ]

        #region [ ExecuteScalar method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes the scalar operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   An object. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static object ExecuteScalar(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            object res = null;

            try
            {
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;
                    command.CommandType = commandType;

                    if (transaction != null)
                        command.Transaction = transaction;

                    DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                    res = command.ExecuteScalar();

                    if (outputParameters != null && outputParameters.Count > 0)
                        outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
                }
            }
            catch (Exception e)
            {
                res = -1;
                throw;
            }

            return res;
        }

        #endregion [ ExecuteScalar method ]

        #region [ GetResultSet method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets result set of sql query. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   The DataSet object instance. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static DataSet GetResultSet(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            DataSet dataset = null;

            try
            {
                Type adapterType = null;

                var adapterTypes =
                     connection.GetType().Assembly.GetExportedTypes().Where(
                         typ => typ.IsClass && typ.GetInterfaces().Contains(typeof(IDbDataAdapter))
                                && typ.IsAbstract == false && typeof(IDbDataAdapter).IsAssignableFrom(typ));

                if (adapterTypes.Count() > 1)
                {
                    var connTypeName = connection.GetType().Name;

                    if (connTypeName.EndsWith("Connection"))
                        connTypeName = connTypeName.Substring(0, connTypeName.Length - 10).ToLower();

                    adapterType = adapterTypes.Where(typ => typ.Name.ToLower().StartsWith(connTypeName)).First();
                }
                else
                {
                    adapterType = adapterTypes.First();
                }

                if (adapterType != null)
                {
                    var dataAdapter = Activator.CreateInstance(adapterType) as IDbDataAdapter;

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        command.CommandType = commandType;

                        if (transaction != null)
                            command.Transaction = transaction;

                        DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                        dataAdapter.SelectCommand = command;
                        dataset = new DataSet();
                        var result = dataAdapter.Fill(dataset);

                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return dataset;
        }

        #endregion [ GetResultSet method ]

        #region [ GetDynamicResultSet method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets dynamic result set. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   The dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<ExpandoObject> GetDynamicResultSet(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);

                        list = reader.GetDynamicResultSet(closeAtFinal: true);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }

            return list;
        }

        #endregion [ GetDynamicResultSet method ]

        #region [ GetDynamicResultSetWithPaging method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that gets dynamic result set with paging.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        /// <param name="pageNumber">       (Optional) The page number. </param>
        /// <param name="pageItemCount">    (Optional) Number of page ıtems. </param>
        ///
        /// <returns>   The dynamic result set with paging. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<ExpandoObject> GetDynamicResultSetWithPaging(this IDbConnection connection,
            string sql, CommandType commandType,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null,
            uint pageNumber = 1, uint pageItemCount = 10)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);

                        list = reader.GetDynamicResultSetWithPaging(
                            pageNumber: pageNumber, pageItemCount: pageItemCount, closeAtFinal: false);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        if (reader != null && !reader.IsClosed)
                            reader.Close();
                    }
                }
            }

            return list;
        }

        #endregion [ GetDynamicResultSetWithPaging method ]

        #region [ GetMultiDynamicResultSet method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that gets multi dynamic result set. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sql">              The SQL. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   The multi dynamic result set. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<List<ExpandoObject>> GetMultiDynamicResultSet(this IDbConnection connection,
            string sql, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            var list = new List<List<ExpandoObject>>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);

                        list = reader.GetMultiDynamicResultSet(closeAtFinal: true);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        reader.CloseIfNot();
                    }
                }
            }

            return list;
        }

        #endregion [ GetMultiDynamicResultSet method ]

        #region [ ExecuteScalarAs method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDbConnection extension method that executes the scalar as operation.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sqlText">          The SQL text. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   A T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static T ExecuteScalarAs<T>(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null) where T : struct
        {
            var value = ExecuteScalar(
                connection, sqlText, commandType, transaction, inputParameters, outputParameters);

            return !value.IsNullOrDbNull() ? (T)value : default(T);
        }

        #endregion [ ExecuteScalarAs method ]

        #region [ ExecuteAsLong method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes as long operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sqlText">          The SQL text. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   A long. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static long ExecuteAsLong(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null)
        {
            var value = Execute(
                connection, sqlText, commandType, transaction, inputParameters, outputParameters);

            return (long)value;
        }

        #endregion [ ExecuteAsLong method ]

        #region [ ExecuteAsDecimal method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that executes as decimal operation. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sqlText">          The SQL text. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   A decimal. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static decimal ExecuteAsDecimal(this IDbConnection connection,
           string sqlText, CommandType commandType = CommandType.Text,
           IDbTransaction transaction = null,
           Dictionary<string, object> inputParameters = null,
           Dictionary<string, object> outputParameters = null)
        {
            var value = Execute(
                connection, sqlText, commandType, transaction, inputParameters, outputParameters);

            return (decimal)value;
        }

        #endregion [ ExecuteAsDecimal method ]

        #region [ FirstAsDynamic method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that first as dynamic. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sqlText">          The SQL text. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   An ExpandoObject. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static ExpandoObject FirstAsDynamic(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            ExpandoObject expando;

            using (var reader = ExecuteReader(
                connection, sqlText, commandType,
                transaction, inputParameters, outputParameters))
            {
                try
                {
                    expando = reader.FirstRow();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    reader.CloseIfNot();
                }
            }

            return expando;
        }

        #endregion [ FirstAsDynamic method ]

        #region [ First method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that firsts. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sqlText">          The SQL text. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   A T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static T First<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null) where T : class, new()
        {
            T instance = null;

            try
            {
                ExpandoObject expando = FirstAsDynamic(
                    connection, sqlText, commandType,
                    transaction, inputParameters, outputParameters);

                instance = DynamicExtensions.ConvertToInstance<T>(expando);
            }
            catch (Exception e)
            {
                throw;
            }

            return instance;
        }

        #endregion [ First method ]

        #region [ LastAsDynamic method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that last as dynamic. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sqlText">          The SQL text. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   An ExpandoObject. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static ExpandoObject LastAsDynamic(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            ExpandoObject expando;

            using (var reader = ExecuteReader(
                connection, sqlText, commandType,
                transaction, inputParameters, outputParameters))
            {
                try
                {
                    expando = reader.LastRow();
                }
                catch (Exception e)
                {
                    throw;
                }
                finally
                {
                    reader.CloseIfNot();
                }
            }

            return expando;
        }

        #endregion [ LastAsDynamic method ]

        #region [ Last method ]

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An IDbConnection extension method that lasts. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="connection">       The connection to act on. </param>
        /// <param name="sqlText">          The SQL text. </param>
        /// <param name="commandType">      (Optional) Type of the command. </param>
        /// <param name="transaction">      (Optional) The transaction. </param>
        /// <param name="inputParameters">  (Optional) Options for controlling the input. </param>
        /// <param name="outputParameters"> (Optional) Options for controlling the output. </param>
        ///
        /// <returns>   A T. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static T Last<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null) where T : class, new()
        {
            T instance = null;

            try
            {
                ExpandoObject expando = LastAsDynamic(
                    connection, sqlText, commandType,
                    transaction, inputParameters, outputParameters);

                instance = DynamicExtensions.ConvertToInstance<T>(expando);
            }
            catch (Exception e)
            {
                throw;
            }

            return instance;
        }

        #endregion [ Last method ]

        #region [ GetList method ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="inputParameters"></param>
        /// <param name="outputParameters"></param>
        /// <returns></returns>
        public static List<T> GetList<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null) where T : class
        {
            var dynList = GetDynamicResultSet(connection, sqlText, commandType, transaction, inputParameters, outputParameters);
            var resultSet = DynamicExtensions.ConvertToList<T>(dynList);
            return resultSet;
        }

        #endregion

        #region [ GetDynamicResultSetSkipAndTake method ]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="inputParameters"></param>
        /// <param name="outputParameters"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static List<ExpandoObject> GetDynamicResultSetSkipAndTake(this IDbConnection connection,
            string sql, CommandType commandType,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null,
            uint skip = 0, uint take = 0)
        {
            List<ExpandoObject> list = new List<ExpandoObject>();

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.CommandType = commandType;

                if (transaction != null)
                    command.Transaction = transaction;

                DxDbCommandHelper.SetCommandParameters(command, inputParameters, outputParameters);

                using (var reader = command.ExecuteReader())
                {
                    try
                    {
                        if (outputParameters != null && outputParameters.Count > 0)
                            outputParameters = (Dictionary<string, object>)DxDbCommandHelper.GetOutParametersOfCommand(command);

                        list = reader.GetDynamicResultSetSkipAndTake(skip: skip, take: take, closeAtFinal: false);
                    }
                    catch (Exception e)
                    {
                        throw;
                    }
                    finally
                    {
                        reader.CloseIfNot();
                    }
                }
            }

            return list;
        }

        #endregion

        #region [ GetListSkipAndTake method ]

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="sqlText"></param>
        /// <param name="commandType"></param>
        /// <param name="transaction"></param>
        /// <param name="inputParameters"></param>
        /// <param name="outputParameters"></param>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static List<T> GetListSkipAndTake<T>(this IDbConnection connection,
            string sqlText, CommandType commandType = CommandType.Text,
            IDbTransaction transaction = null,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null,
            uint skip = 0, uint take = 0) where T : class, new()
        {
            var dynList = GetDynamicResultSetSkipAndTake(connection, sqlText, commandType, transaction, inputParameters, outputParameters, skip: skip, take: take);
            var resultSet = DynamicExtensions.ConvertToList<T>(dynList);
            return resultSet;
        }

        #endregion
    }
}