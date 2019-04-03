namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Reflection;

    public static class DynamicExtensions
    {
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

            instance = Activator.CreateInstance<T>();
            var props = typeof(T).GetProperties();

            props = props.AsQueryable().Where(p => p.CanWrite == true).ToArray();
            props = props.AsQueryable().Where(p => dict.ContainsKey(p.Name) == true).ToArray();

            foreach (var prp in props)
            {
                prp.SetValue(instance, dict[prp.Name]);
            }

            return instance;
        }

        public static T ConvertToV2<T>(this ExpandoObject dyn) where T : class
        {
            if (dyn == null)
            {
                throw new ArgumentNullException(nameof(dyn));
            }

            IDictionary<string, string> columns = typeof(T).GetColumnsOfTypeAsReverse();

            T instance = null;

            IDictionary<string, object> dict = dyn;

            if ((dict?.Count).GetValueOrDefault(0) < 1)
                return instance;

            instance = Activator.CreateInstance<T>();
            PropertyInfo[] pInfos = typeof(T).GetValidPropertiesOfType();

            pInfos = pInfos.AsQueryable().Where(q => columns.Values.Contains(q.Name) == true).ToArray();

            foreach (var prp in pInfos)
            {
                prp.SetValue(instance, dict[prp.Name]);
            }

            return instance;
        }

        public static List<T> ConvertToList<T>(this List<ExpandoObject> dynList) where T : class
        {
            if (dynList == null)
            {
                throw new ArgumentNullException(nameof(dynList));
            }

            List<T> list = new List<T>();

            IDictionary<string, string> columns = typeof(T).GetColumnsOfType();

            PropertyInfo[] pInfos = typeof(T).GetValidPropertiesOfType();
            pInfos = pInfos.AsQueryable().Where(q => columns.Keys.Contains(q.Name) == true).ToArray();

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
                    prp.SetValue(instance, dict[col]);
                }
                list.Add(instance);
            }
            return list;
        }

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
                result =
                    dictionary[key] == (object)DBNull.Value ? null : dictionary[key];
            }

            return result;
        }

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
                dictionary[key] =
                    value == (object)DBNull.Value ? null : value;
                expandoObj = dictionary as ExpandoObject;
                result = true;
            }

            return result;
        }
    }
}