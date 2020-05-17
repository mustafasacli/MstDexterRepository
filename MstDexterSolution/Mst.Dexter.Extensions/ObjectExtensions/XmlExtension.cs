namespace Mst.Dexter.Extensions.ObjectExtensions
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Xml;

    public static class XmlExtension
    {
        /// <summary>
        /// Get xml nodes as expando object list.
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns>expando object list</returns>
        public static List<ExpandoObject> GetNodesAsDynamic(this XmlNodeList nodeList)
        {
            var list = new List<ExpandoObject>();

            if (nodeList == null || nodeList.Count < 1)
                return list;
            foreach (XmlNode node in nodeList)
            {
                var childNodes = node.ChildNodes;
                if (childNodes == null)
                    continue;

                IDictionary<string, object> dictionary = new ExpandoObject();
                foreach (XmlNode childNode in childNodes)
                {
                    dictionary[childNode.Name] = childNode.InnerXml;
                }

                ExpandoObject expando = dictionary as ExpandoObject;
                list.Add(expando);
            }

            return list;
        }
    }
}
