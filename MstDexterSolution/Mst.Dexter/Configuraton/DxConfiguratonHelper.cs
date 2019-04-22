namespace Mst.Dexter.Configuraton
{
    using System;
    using System.Configuration;
    using System.Xml;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dx configuraton helper. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    internal static class DxConfiguratonHelper
    {
        private static object syncObj = new object();
        private static XmlNode mainNode = null;

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
                        mainNode = ConfigurationManager.GetSection(AppValues.ConfigMainSectionName) as XmlNode;
                    }
                }
            }

            return mainNode;
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
                nodeList = GetMainNode()?.SelectNodes(AppValues.ConfigAddSectionName);
            }
            catch (Exception e)
            {
                throw;
            }

            return nodeList;
        }

        //writeEventLog
        /// <summary>
        /// Checks events will be logged.
        /// </summary>
        public static bool IsWriteEventLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetMainNode()?.Attributes[AppValues.IsWriteEventLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppValues.One;

                return b;
            }
        }

        //writeErrorLog
        /// <summary>
        /// Checks erors will be logged.
        /// </summary>
        public static bool IsWriteErrorLog
        {
            get
            {
                bool b = false;

                XmlAttribute attr = GetMainNode()?.Attributes[AppValues.IsWriteErrorLogAttribute];

                string s = attr?.Value;
                s = s ?? string.Empty;
                s = s.Trim();
                b = s == AppValues.One;

                return b;
            }
        }
    }
}
