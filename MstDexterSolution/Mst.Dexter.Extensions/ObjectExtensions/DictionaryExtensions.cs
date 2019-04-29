namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A dictionary extensions. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class DictionaryExtensions
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An IDictionary&lt;string,T&gt; extension method that gets value or default.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <exception cref="ArgumentNullException"> Thrown when one or more required arguments are null. </exception>
        ///
        /// <typeparam name="T">    Generic type parameter. </typeparam>
        /// <param name="dictionary">   The dictionary to act on. </param>
        /// <param name="key">          The key. </param>
        /// <param name="defaultValue"> The default value. </param>
        ///
        /// <returns>   The value or default. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// A Dictionary&lt;string,string&gt; extension method that dictionary to list.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="dictionary">   The dictionary to act on. </param>
        ///
        /// <returns>   A List&lt;string&gt; </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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