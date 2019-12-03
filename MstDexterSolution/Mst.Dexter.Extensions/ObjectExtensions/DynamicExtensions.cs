namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dynamic extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DynamicExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An ExpandoObject extension method that convert to. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="dyn">  The dyn to act on. </param>
        ///
        /// <returns>   to converted. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static T ConvertTo<T>(this ExpandoObject dyn) where T : class
        {
            if (dyn == null)
            {
                throw new ArgumentNullException(nameof(dyn));
            }

            T instance = null;

            IDictionary<string, object> dict = dyn;

            if ((dict?.Count).GetValueOrDefault(0) < 1)
                return instance;

            IDictionary<string, string> columns = new Dictionary<string, string>();

            {
                var cols = typeof(T).GetColumnsOfType(includeNotMappedProperties: true) ?? new Dictionary<string, string>();
                cols.Where(q => dict.ContainsKey(q.Value)).ToList().ForEach(q => columns[q.Key] = q.Value);
            }

            instance = Activator.CreateInstance<T>();
            PropertyInfo[] pInfos = typeof(T).GetValidPropertiesOfType(includeNotMappedProperties: true);

            pInfos = pInfos
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray();

            foreach (var prp in pInfos)
            {
                prp.SetValue(instance, dict[columns[prp.Name]].GetValueWithCheckDbNull());
            }

            return instance;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An ExpandoObject extension method that converts a dyn to a v 2. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="dyn">  The dyn to act on. </param>
        ///
        /// <returns>   The given data converted to a v 2. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static T ConvertToV2<T>(this ExpandoObject dyn) where T : class
        {
            if (dyn == null)
            {
                throw new ArgumentNullException(nameof(dyn));
            }

            IDictionary<string, string> columns = typeof(T).GetColumnsOfType(includeNotMappedProperties: true);

            T instance = null;

            IDictionary<string, object> dict = dyn;

            if ((dict?.Count).GetValueOrDefault(0) < 1)
                return instance;

            instance = Activator.CreateInstance<T>();
            PropertyInfo[] pInfos = typeof(T).GetValidPropertiesOfType(includeNotMappedProperties: true);

            pInfos = pInfos
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray();

            foreach (var prp in pInfos)
            {
                prp.SetValue(instance, dict[columns[prp.Name]].GetValueWithCheckDbNull());
            }

            return instance;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// A List&lt;ExpandoObject&gt; extension method that converts a dynList to a list.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="dynList">  The dynList to act on. </param>
        ///
        /// <returns>   The given data converted to a list. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static List<T> ConvertToList<T>(this List<ExpandoObject> dynList) where T : class
        {
            if (dynList == null)
            {
                throw new ArgumentNullException(nameof(dynList));
            }

            List<T> list = new List<T>();

            if (dynList.Count < 1)
                return list;

            IDictionary<string, string> columns = typeof(T).GetColumnsOfType(includeNotMappedProperties: true);

            PropertyInfo[] pInfos = typeof(T).GetValidPropertiesOfType(includeNotMappedProperties: true);
            pInfos = pInfos
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray() ?? new PropertyInfo[0];

            if (pInfos.Length < 1)
                return list;

            IDictionary<string, object> dict;
            T instance;
            string col;

            foreach (var dyn in dynList)
            {
                dict = dyn;
                if ((dict?.Count).GetValueOrDefault(0) < 1)
                    continue;

                instance = Activator.CreateInstance<T>();

                foreach (var prp in pInfos)
                {
                    col = null;
                    col = columns[prp.Name];
                    if (dict.ContainsKey(col))
                        prp.SetValue(instance, dict[col].GetValueWithCheckDbNull());
                }

                list.Add(instance);
            }

            return list;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An ExpandoObject extension method that gets a value. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="expandoObj">   The expandoObj to act on. </param>
        /// <param name="key">          The key. </param>
        ///
        /// <returns>   The value. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static object GetValue(this ExpandoObject expandoObj, string key)
        {
            if (expandoObj == null)
                throw new ArgumentNullException(nameof(expandoObj));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var dictionary = (expandoObj as IDictionary<string, object>)
                ?? new Dictionary<string, object>();

            object result = null;
            key = key.Trim();

            if (dictionary.Count > 0 && dictionary.ContainsKey(key))
            {
                result = dictionary[key].GetValueWithCheckDbNull();
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An ExpandoObject extension method that sets a value. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are
        ///                                             null. </exception>
        ///
        /// <param name="expandoObj">   The expandoObj to act on. </param>
        /// <param name="key">          The key. </param>
        /// <param name="value">        The value. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool SetValue(this ExpandoObject expandoObj, string key, object value)
        {
            if (expandoObj == null)
                throw new ArgumentNullException(nameof(expandoObj));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var dictionary = (expandoObj as IDictionary<string, object>)
                ?? new Dictionary<string, object>();

            var result = false;
            key = key.Trim();

            if (dictionary.Count > 0 && dictionary.ContainsKey(key))
            {
                dictionary[key] = value.GetValueWithCheckDbNull();
                expandoObj = dictionary as ExpandoObject;
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Extension method that turns a dictionary of string and object to an ExpandoObject
        /// </summary>
        public static ExpandoObject ToExpando(this IDictionary<string, object> dictionary)
        {
            var expando = new ExpandoObject();
            var expandoDic = (IDictionary<string, object>)expando;

            // go through the items in the dictionary and copy over the key value pairs)

            foreach (var kvp in dictionary)
            {
                // if the value can also be turned into an ExpandoObject, then do it!

                if (kvp.Value is IDictionary<string, object>)
                {
                    var expandoValue = ((IDictionary<string, object>)kvp.Value).ToExpando();

                    expandoDic.Add(kvp.Key, expandoValue);
                }
                else if (kvp.Value is ICollection)
                {
                    // iterate through the collection and convert any strin-object dictionaries
                    // along the way into expando objects

                    var itemList = new List<object>();

                    foreach (var item in (ICollection)kvp.Value)
                    {
                        if (item is IDictionary<string, object>)
                        {
                            var expandoItem = ((IDictionary<string, object>)item).ToExpando();

                            itemList.Add(expandoItem);
                        }
                        else
                        {
                            itemList.Add(item);
                        }
                    }

                    expandoDic.Add(kvp.Key, itemList);
                }
                else
                {
                    expandoDic.Add(kvp);
                }
            }

            return expando;
        }
    }
}