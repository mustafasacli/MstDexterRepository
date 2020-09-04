namespace Mst.DexterCfg.Configuraton
{
    using System;
    using System.Xml;
    using System.Reflection;
    using System.IO;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dx configuration configuraton helper. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    internal static class DxCfgConfiguratonHelper
    {
        private static readonly object syncObj = new object();
        private static XmlNode mainNode = null;

        private static readonly object syncObj2 = new object();
        private static XmlNode settingNode = null;

        private static readonly object docSyncObj = new object();
        private static XmlDocument mainDocument = null;

        internal static XmlNode MainDocument
        {
            get
            {
                if (mainDocument == null)
                {
                    lock (docSyncObj)
                    {
                        if (mainDocument == null)
                        {
                            mainDocument = new XmlDocument();
                            mainDocument.Load(Path.Combine(DllPath, AppCfgValues.ConfigFile));
                        }
                    }
                }

                return mainDocument;
            }
        }

        /// <summary>
        /// Get Connection Main Node.
        /// </summary>
        /// <returns>Returns XmlNode object.</returns>
        internal static XmlNode GetMainNode()
        {
            if (mainNode == null)
            {
                lock (syncObj)
                {
                    if (mainNode == null)
                    {
                        mainNode = MainDocument.SelectSingleNode(AppCfgValues.ConfigMainNodeName);
                    }
                }
            }

            return mainNode;
        }

        /// <summary>
        /// Get Setting Main Node.
        /// </summary>
        /// <returns>Returns XmlNode object.</returns>
        internal static XmlNode GetSettingNode()
        {
            if (settingNode == null)
            {
                lock (syncObj2)
                {
                    if (settingNode == null)
                    {
                        settingNode = MainDocument.SelectSingleNode(AppCfgValues.ConfigSettingNodesName);
                    }
                }
            }

            return settingNode;
        }

        /// <summary>
        /// Get Connection List as XmlNodeList.
        /// </summary>
        /// <returns>Returns XmlNodeList object</returns>
        public static XmlNodeList GetConnectionNodeList()
        {
            XmlNodeList nodeList = null;

            try
            {
                nodeList = GetMainNode().SelectNodes(AppCfgValues.ConfigAddSectionName);
            }
            catch (Exception e)
            {
                throw;
            }

            return nodeList;
        }

        /// <summary>
        /// Checks events will be logged.
        /// </summary>
        public static bool IsWriteEventLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetMainNode().Attributes[AppCfgValues.IsWriteEventLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }

        /// <summary>
        /// Checks erors will be logged.
        /// </summary>
        public static bool IsWriteErrorLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetMainNode().Attributes[AppCfgValues.IsWriteErrorLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }

        /// <summary>
        /// Checks events will be logged.
        /// </summary>
        public static bool IsWriteSettingEventLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetSettingNode().Attributes[AppCfgValues.IsWriteEventLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }

        /// <summary>
        /// Checks erors will be logged.
        /// </summary>
        public static bool IsWriteSettingErrorLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetSettingNode().Attributes[AppCfgValues.IsWriteErrorLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppCfgValues.One;

                return b;
            }
        }

        internal static string DllPath
        {
            get
            {
                //Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string assemblyFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                if (assemblyFolder.ToLowerInvariant().StartsWith("file:"))
                {
                    assemblyFolder = assemblyFolder.Substring(5);
                    assemblyFolder = assemblyFolder.TrimStart('\\');
                }
                return assemblyFolder;
            }
        }
    }
}