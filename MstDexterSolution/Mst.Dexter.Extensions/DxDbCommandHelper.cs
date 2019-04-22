namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Dynamic;
    using System.Linq;

    public partial class DxDbCommandHelper
    {
        /// <summary>
        /// creates parameters and sets their values of IDbCommand.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="inputParameters"></param>
        /// <param name="outputParameters"></param>
        public static void SetCommandParameters(IDbCommand command,
            Dictionary<string, object> inputParameters = null,
            Dictionary<string, object> outputParameters = null)
        {
            Dictionary<string, object> inputs =
                inputParameters ?? new Dictionary<string, object>();

            Dictionary<string, object> outputs =
                outputParameters ?? new Dictionary<string, object>();
            IDbDataParameter parameter;

            foreach (var key in inputs)
            {
                parameter = command.CreateParameter();
                parameter.ParameterName = key.Key;
                parameter.Value = key.Value;

                parameter.Direction = outputs.ContainsKey(key.Key) ?
                    ParameterDirection.InputOutput : ParameterDirection.Input;

                command.Parameters.Add(parameter);
            }

            foreach (var key in outputs)
            {
                if (!inputs.ContainsKey(key.Key))
                {
                    parameter = command.CreateParameter();
                    parameter.ParameterName = key.Key;
                    parameter.Direction = ParameterDirection.Output;
                    command.Parameters.Add(parameter);
                }
            }
        }

        /// <summary>
        /// Sets parameters of DbCommand
        /// </summary>
        /// <param name="command">Db command</param>
        /// <param name="parameters">Db Command Parameters</param>
        public static void SetCommandParameters(IDbCommand command,
           params object[] parameters)
        {
            if (parameters == null || parameters.Length < 1)
                return;

            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }
        }

        /// <summary>
        /// Get Output Values of IDbCommand
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetOutParametersOfCommand(IDbCommand command)
        {
            IDictionary<string, object> outputParameters = new Dictionary<string, object>();

            if (command.Parameters != null && command.Parameters.Count > 0)
            {
                IDbDataParameter prm;
                // outputParameters = new Dictionary<string, object>();
                foreach (var item in command.Parameters)
                {
                    prm = item as IDbDataParameter;
                    if (prm.Direction.IsMember(
                        ParameterDirection.Output,
                        ParameterDirection.InputOutput,
                        ParameterDirection.ReturnValue))
                    {
                        outputParameters[prm.ParameterName] = prm.Value;
                    }
                }
            }

            return outputParameters;
        }
    }
}