////////////////////////////////////////////////////////////////////////////////////////////////////
// file:	Models\MstDictionary.cs
//
// summary:	Implements the mst dictionary class
////////////////////////////////////////////////////////////////////////////////////////////////////

namespace Mst.Dexter.Extensions.Objects
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   MstDictionary. This class cannot be inherited. </summary>
    ///
    /// <remarks>   Msacli, 7.05.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public sealed class MstDictionary : IDictionary<string, object>
    {
        /// <summary>
        /// The dictionary.
        /// </summary>
        private Dictionary<string, object> dictionary = null;

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Default constructor. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public MstDictionary()
        {
            dictionary = new Dictionary<string, object>();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="dict">   The dictionary. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public MstDictionary(Dictionary<string, object> dict) : this()
        {
            if (dict != null)
            {
                foreach (var key in dict.Keys)
                {
                    dictionary[key] = dict[key];
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="dict">   The dictionary. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public MstDictionary(IDictionary<string, object> dict) : this()
        {
            if (dict != null)
            {
                foreach (var key in dict.Keys)
                {
                    dictionary[key] = dict[key];
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Constructor. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="expando">  The expando. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public MstDictionary(ExpandoObject expando) : this()
        {
            if (expando != null)
            {
                var dict = expando as IDictionary<string, object>;
                foreach (var key in dict.Keys)
                {
                    dictionary[key] = dict[key];
                }
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets or sets the element with the specified key. </summary>
        ///
        /// <param name="key">  The key of the element to get or set. </param>
        ///
        /// <returns>   The element with the specified key. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public object this[string key]
        {
            get
            {
                return dictionary[key];
            }

            set
            {
                dictionary[key] = value;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the number of.  </summary>
        ///
        /// <value> The count. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets a value indicating whether the ıs read only. </summary>
        ///
        /// <value> True if ıs read only, false if not. </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool IsReadOnly
        {
            get
            {
                return (dictionary as IDictionary<string, object>).IsReadOnly;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the
        /// <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        ///
        /// <value>
        /// An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the
        /// object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICollection<string> Keys
        {
            get
            {
                return dictionary.Keys;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in
        /// the <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        ///
        /// <value>
        /// An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the
        /// object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </value>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public ICollection<object> Values
        {
            get
            {
                return dictionary.Values;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adds an element with the provided key and value to the
        /// <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="item"> The item to remove. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add(KeyValuePair<string, object> item)
        {
            dictionary.Add(item.Key, item.Value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adds an element with the provided key and value to the
        /// <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="key">      The object to use as the key of the element to add. </param>
        /// <param name="value">    The object to use as the value of the element to add. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add(string key, object value)
        {
            dictionary.Add(key, value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Clears this object to its blank/initial state. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clear()
        {
            dictionary.Clear();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Query if this object contains the given item. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="item"> The item to remove. </param>
        ///
        /// <returns>   True if the object is in this collection, false if not. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Contains(KeyValuePair<string, object> item)
        {
            var result = false;

            result = dictionary.Contains(item);

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an
        /// element with the specified key.
        /// </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="key">  The key to locate in the
        ///                     <see cref="T:System.Collections.Generic.IDictionary`2" />. </param>
        ///
        /// <returns>
        /// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element
        /// with the key; otherwise, false.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool ContainsKey(string key)
        {
            return dictionary.ContainsKey(key);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Copies to. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="array">        The array. </param>
        /// <param name="arrayIndex">   The array ındex. </param>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
        {
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the enumerator. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <returns>   The enumerator. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the element with the specified key from the
        /// <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="item"> The item to remove. </param>
        ///
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns
        /// false if <paramref name="key" /> was not found in the original
        /// <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Remove(KeyValuePair<string, object> item)
        {
            return dictionary.Remove(item.Key);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Removes the element with the specified key from the
        /// <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="key">  The key of the element to remove. </param>
        ///
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.  This method also returns
        /// false if <paramref name="key" /> was not found in the original
        /// <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Remove(string key)
        {
            return dictionary.Remove(key);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the value associated with the specified key. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <param name="key">      The key whose value to get. </param>
        /// <param name="value">    [out] When this method returns, the value associated with the
        ///                         specified key, if the key is found; otherwise, the default value for the
        ///                         type of the <paramref name="value" /> parameter. This parameter is passed
        ///                         uninitialized. </param>
        ///
        /// <returns>
        /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />
        /// contains an element with the specified key; otherwise, false.
        /// </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool TryGetValue(string key, out object value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the enumerator. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <returns>   The enumerator. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        IEnumerator IEnumerable.GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   Gets the content. </summary>
        ///
        /// <remarks>   Msacli, 7.05.2019. </remarks>
        ///
        /// <returns>   The content. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public Dictionary<string, object> AsDictionary()
        {
            return dictionary;
        }
    }
}