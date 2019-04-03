using System;
using System.Collections;
using System.Xml;

namespace Mst.Dexter.PocoGenerator.Source.Settings
{
    internal class Setting
    {
        public static readonly string xmlFile = "conf.xml";

        private readonly Hashtable hash;

        public Setting()
        {
            try
            {
                this.hash = this.GetConnTables();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public object this[string key]
        {
            get
            {
                if (this.hash.ContainsKey(key))
                    return this.hash[key];
                else
                    throw new InvalidOperationException("Invalid key.");
            }
        }

        private Hashtable GetConnTables()
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlFile);
                XmlNodeList nodeList = xmldoc.SelectNodes("app/app-conn-settings/connectiontypes/connectiontype");
                Hashtable tmpTable = new Hashtable();
                foreach (XmlNode node in nodeList)
                {
                    tmpTable.Add(
                        node.SelectSingleNode("name").InnerText,
                        node.SelectSingleNode("connectionstring").InnerText);
                }
                return tmpTable;
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
            catch (Exception e)
            {
                GenerateXmlDocument();
                return GetConnTables();
            }
        }

        private void GenerateXmlDocument()
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                XmlNode appNode = xmlDoc.CreateElement("app");
                xmlDoc.AppendChild(appNode);
                XmlNode appConnNode = xmlDoc.CreateElement("app-conn-settings");
                appNode.AppendChild(appConnNode);
                XmlNode mainNode = xmlDoc.CreateElement("connectiontypes");
                appConnNode.AppendChild(mainNode);
                // Array enumArray = Enum.GetValues(typeof(RcConnectionTypes));
                // XmlNode connTypeNode;
                // foreach (var item in ConnTypes)
                // {

                /// TODO MST: WILL CHECK
                // foreach (var item in Enum.GetValues(typeof(RcConnectionTypes)))
                // {
                //    connTypeNode = GenerateXmlNode(xmlDoc, item.ToString());
                //    mainNode.AppendChild(connTypeNode);
                // }

                XmlNode langNode = xmlDoc.CreateElement("app-lang");
                langNode.InnerText = "tr-TR";

                appNode.AppendChild(langNode);

                xmlDoc.Save(xmlFile);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private XmlNode GenerateXmlNode(XmlDocument xmlDoc, string connTypeName)
        {
            try
            {
                XmlNode connTypeNode = xmlDoc.CreateElement("connectiontype");
                XmlNode connTypeNameNode = xmlDoc.CreateElement("name");
                XmlNode connStringNode = xmlDoc.CreateElement("connectionstring");

                connTypeNameNode.InnerText = connTypeName;
                connStringNode.InnerText = string.Format("Connection string Of {0} Connection Type", connTypeName);
                connTypeNode.AppendChild(connTypeNameNode);
                connTypeNode.AppendChild(connStringNode);

                return connTypeNode;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void SaveSetting(string key, string value)
        {
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(xmlFile);
                XmlNodeList nodeList = xmldoc.SelectNodes("app/app-conn-settings/connectiontypes/connectiontype");

                foreach (XmlNode node in nodeList)
                {
                    if (node.SelectSingleNode("name").InnerText.Equals(key))
                    {
                        node.SelectSingleNode("connectionstring").InnerText = value;
                        break;
                    }
                }
                xmldoc.Save(xmlFile);
            }
            catch
            {
                this.GenerateXmlDocument();
                this.SaveSetting(key, value);
            }
        }
    }
}