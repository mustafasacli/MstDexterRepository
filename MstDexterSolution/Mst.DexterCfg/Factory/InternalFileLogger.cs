namespace Mst.DexterCfg.Factory
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class CfgFileOperator
    {
        private static Lazy<CfgFileOperator> lazyOp =
            new Lazy<CfgFileOperator>(() => new CfgFileOperator());

        private CfgFileOperator()
        {
        }

        public static CfgFileOperator Instance
        { get { return lazyOp.Value; } }

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