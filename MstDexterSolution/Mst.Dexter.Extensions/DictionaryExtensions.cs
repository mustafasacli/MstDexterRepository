namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class DictionaryExtensions
    {
        public static T GetValueOrDefault<T>(this IDictionary<string, T> dictionary, string key, T defaultValue)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));

            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            T val;

            if (dictionary.ContainsKey(key))
                val = dictionary[key];
            else
                val = defaultValue;

            return val;
        }

        public static List<string> DictionaryToList(this Dictionary<string, string> dictionary)
        {
            var list = new List<string> { };

            if (dictionary == null)
                return list;

            dictionary
                .Keys
                .ToList()
                .ForEach(q =>
                {
                    list.Add($"{q} : {dictionary[q]}");
                });

            return list;
        }
    }
}