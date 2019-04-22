namespace Mst.Dexter.Factory
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A file operator. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    internal class FileOperator
    {
        private static Lazy<FileOperator> lazyOp =
            new Lazy<FileOperator>(() => new FileOperator());

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Constructor that prevents a default instance of this class from being created.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        private FileOperator()
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the instance. </summary>
        ///
        /// <value> The instance. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static FileOperator Instance
        { get { return lazyOp.Value; } }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Writes the given file. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="filePath">     Full pathname of the file. </param>
        /// <param name="rows">         The rows. </param>
        /// <param name="writeLine">    (Optional) True to write line. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Write(string filePath, List<string> rows, bool writeLine = false)
        {
            try
            {
                if (rows == null || rows.Count < 1)
                    return;

                var fileMode = File.Exists(filePath) ? FileMode.Append : FileMode.OpenOrCreate;

                using (StreamWriter writer = new StreamWriter(
                           new FileStream(filePath, fileMode))
                { AutoFlush = true })
                {
                    if (!writeLine)
                    {
                        string str;
                        rows.ForEach(s =>
                        {
                            str = s ?? string.Empty;
                            if (str.EndsWith("\r\n") || str.EndsWith("\n"))
                            {
                                writer.Write(str);
                            }
                            else
                            {
                                writer.WriteLine(str);
                            }
                        });
                    }
                    else
                    {
                        rows.ForEach(s =>
                        {
                            writer.WriteLine(s ?? string.Empty);
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}