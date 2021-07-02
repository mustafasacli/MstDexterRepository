using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Mst.Dexter.Extensions
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dynamic extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DynamicExtensions
    {
        /// <summary>
        /// Convert dynamic object to new T object instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter. .</typeparam>
        /// <param name="expandoObject">The dyn to act on. .</param>
        /// <returns>to converted. .</returns>
        public static T ConvertToInstance<T>(this ExpandoObject expandoObject) where T : class
        {
            if (expandoObject == null)
            {
                throw new ArgumentNullException(nameof(expandoObject));
            }

            T instance = null;

            IDictionary<string, object> values = expandoObject;

            if ((values?.Count).GetValueOrDefault(0) < 1)
                return instance;

            IDictionary<string, string> columns = new Dictionary<string, string>();

            {
                var cols = typeof(T).GetColumnsOfType(includeNotMappedProperties: true) ?? new Dictionary<string, string>();
                cols.Where(q => values.ContainsKey(q.Value)).ToList().ForEach(q => columns[q.Key] = q.Value);
            }

            instance = Activator.CreateInstance<T>();
            PropertyInfo[] properties = typeof(T).GetValidPropertiesOfType(includeNotMappedProperties: true);

            properties = properties
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray();

            SetPropertyValues(ref instance, properties, values, columns);

            return instance;
        }

        /// <summary>
        /// Convert dynamic object to new T object instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter. .</typeparam>
        /// <param name="dynamicObject">The dyn to act on. .</param>
        /// <returns>to converted. .</returns>
        public static T ConvertTo<T>(dynamic dynamicObject) where T : class
        {
            if (dynamicObject == null)
            {
                throw new ArgumentNullException(nameof(dynamicObject));
            }

            T instance = null;

            IDictionary<string, object> values = (dynamicObject as ExpandoObject);

            if ((values?.Count).GetValueOrDefault(0) < 1)
                return instance;

            IDictionary<string, string> columns = new Dictionary<string, string>();

            {
                var cols = typeof(T).GetColumnsOfType(includeNotMappedProperties: true) ?? new Dictionary<string, string>();
                cols.Where(q => values.ContainsKey(q.Value)).ToList().ForEach(q => columns[q.Key] = q.Value);
            }

            instance = Activator.CreateInstance<T>();
            PropertyInfo[] properties = typeof(T).GetValidPropertiesOfType(includeNotMappedProperties: true);

            properties = properties
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray();

            SetPropertyValues(ref instance, properties, values, columns);

            return instance;
        }

        /// <summary>
        /// Convert dynamic object to new T object instance.
        /// </summary>
        /// <typeparam name="T">Generic type parameter. .</typeparam>
        /// <param name="expandoObject">The dyn to act on. .</param>
        /// <returns>The given data converted to a v 2. .</returns>
        public static T ConvertToV2<T>(this ExpandoObject expandoObject) where T : class
        {
            if (expandoObject == null)
            {
                throw new ArgumentNullException(nameof(expandoObject));
            }

            IDictionary<string, string> columns = typeof(T).GetColumnsOfType(includeNotMappedProperties: true);

            T instance = null;

            IDictionary<string, object> values = expandoObject;

            if ((values?.Count).GetValueOrDefault(0) < 1)
                return instance;

            instance = Activator.CreateInstance<T>();
            PropertyInfo[] properties = typeof(T).GetValidPropertiesOfType(includeNotMappedProperties: true);

            properties = properties
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray();

            SetPropertyValues(ref instance, properties, values, columns);

            return instance;
        }

        /// <summary>
        /// A List&lt;ExpandoObject&gt; extension method that converts a dynList to a list.
        /// </summary>
        /// <typeparam name="T">Generic type parameter. .</typeparam>
        /// <param name="expandoObjectList">The dynList to act on. .</param>
        /// <returns>The given data converted to a list. .</returns>
        public static List<T> ConvertToList<T>(this List<ExpandoObject> expandoObjectList) where T : class
        {
            if (expandoObjectList == null)
                throw new ArgumentNullException(nameof(expandoObjectList));

            List<T> list = new List<T>();

            if (expandoObjectList.Count < 1)
                return list;

            var type = typeof(T);

            IDictionary<string, string> columns = type.GetColumnsOfType(includeNotMappedProperties: true);

            PropertyInfo[] properties = type.GetValidPropertiesOfType(includeNotMappedProperties: true);
            properties = properties
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray() ?? new PropertyInfo[0];

            if (properties.Length < 1)
                return list;

            IDictionary<string, object> values;
            T instance;

            foreach (var expandoObject in expandoObjectList)
            {
                values = expandoObject;
                if ((values?.Count).GetValueOrDefault(0) < 1)
                    continue;

                instance = Activator.CreateInstance<T>();

                SetPropertyValues(ref instance, properties, values, columns);

                list.Add(instance);
            }

            return list;
        }

        /// <summary>
        /// Gets Value from expandoobject with given key.
        /// </summary>
        /// <param name="expandoObject">The expandoObject to act on. .</param>
        /// <param name="key">The key. .</param>
        /// <returns>The value. .</returns>
        public static object GetValue(this ExpandoObject expandoObject, string key)
        {
            if (expandoObject == null)
                throw new ArgumentNullException(nameof(expandoObject));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var dictionary = (expandoObject as IDictionary<string, object>)
                ?? new Dictionary<string, object>();

            object result = null;
            key = key.Trim();

            if (dictionary.Count > 0 && dictionary.ContainsKey(key))
            {
                result = dictionary[key].GetValueWithCheckDbNull();
            }

            return result;
        }

        /// <summary>
        /// Set the value of expando object instance with given parameters.
        /// </summary>
        /// <param name="expandoObject">The expandoObject to act on. .</param>
        /// <param name="key">The key. .</param>
        /// <param name="value">The value. .</param>
        /// <returns>True if it succeeds, false if it fails. .</returns>
        public static bool SetValue(this ExpandoObject expandoObject, string key, object value)
        {
            if (expandoObject == null)
                throw new ArgumentNullException(nameof(expandoObject));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var dictionary = (expandoObject as IDictionary<string, object>)
                ?? new Dictionary<string, object>();

            var result = false;
            key = key.Trim();

            if (dictionary.Count > 0 && dictionary.ContainsKey(key))
            {
                dictionary[key] = value.GetValueWithCheckDbNull();
                expandoObject = dictionary as ExpandoObject;
                result = true;
            }

            return result;
        }

        /// <summary>
        /// A List&lt;ExpandoObject&gt; extension method that converts a dynList to a list.
        /// </summary>
        /// <typeparam name="T">Generic type parameter. .</typeparam>
        /// <param name="dynamicObjectList">The dynList to act on. .</param>
        /// <returns>The given data converted to a list. .</returns>
        public static List<T> ConvertToList<T>(this List<dynamic> dynamicObjectList) where T : class
        {
            if (dynamicObjectList == null)
                throw new ArgumentNullException(nameof(dynamicObjectList));

            List<T> list = new List<T>();

            if (dynamicObjectList.Count < 1)
                return list;

            var type = typeof(T);

            IDictionary<string, string> columns = type.GetColumnsOfType(includeNotMappedProperties: true);

            PropertyInfo[] properties = type.GetValidPropertiesOfType(includeNotMappedProperties: true);
            properties = properties
                .Where(q => columns.Keys.Contains(q.Name) == true)
                .ToArray() ?? new PropertyInfo[0];

            if (properties.Length < 1)
                return list;

            IDictionary<string, object> values;
            T instance;

            foreach (var dynamicObject in dynamicObjectList)
            {
                values = (dynamicObject as ExpandoObject);
                if ((values?.Count).GetValueOrDefault(0) < 1)
                    continue;

                instance = Activator.CreateInstance<T>();

                SetPropertyValues(ref instance, properties, values, columns);

                list.Add(instance);
            }

            return list;
        }

        internal static void SetPropertyValues<T>(ref T instance, PropertyInfo[] properties, IDictionary<string, object> keyValues, IDictionary<string, string> columnPropertyMappings)
        {
            var hasErrorThrowned = false;
            StringBuilder builder = new StringBuilder();

            properties = properties ?? new PropertyInfo[0];

            foreach (var property in properties)
            {
                var columnName = columnPropertyMappings[property.Name];
                if (keyValues.ContainsKey(columnName))
                {
                    var value = keyValues[columnName].GetValueWithCheckDbNull();
                    try
                    {
                        property.SetValue(instance, value);
                    }
                    //catch(ArgumentException age)
                    //{ }
                    catch (Exception e)
                    {
                        hasErrorThrowned = true;
                        builder.Append("Exception-Message: ").AppendLine(e.Message);
                        if (value.IsNullOrDbNull())
                        { builder.AppendLine("Value is null"); }
                        else
                        {
                            builder.Append("Value: ");
                            builder.AppendLine(value.ToStr());

                            builder.Append("Value Type: ");
                            builder.AppendLine((Nullable.GetUnderlyingType(value?.GetType()) ?? value?.GetType())?.Name ?? "");
                        }

                        builder.Append("Property Name: ");
                        builder.AppendLine(property.Name);
                        builder.Append("Property Type: ");
                        builder.AppendLine((Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType).Name);
                        builder.AppendLine("****************************************");
                    }
                }
            }

            if (hasErrorThrowned)
            {
                string message = builder.ToString();
                if (message.IsNullOrSpace() == false)
                    throw new Exception(message);
            }
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