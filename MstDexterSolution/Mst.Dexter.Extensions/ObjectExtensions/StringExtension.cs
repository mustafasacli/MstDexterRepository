﻿namespace Mst.Dexter.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   A string extension. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class StringExtension
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that query if 's' ıs valid. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="s">    The s to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsValid(this string s)
        {
            bool result = false;

            result = !string.IsNullOrWhiteSpace(s);

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that lengths the given s. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="s">    The s to act on. </param>
        ///
        /// <returns>   An int. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int Len(this string s)
        {
            int len = -1;

            if (s != null)
            {
                len = s.Length;
            }

            return len;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that trim all. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="s">    The s to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string TrimAll(this string s)
        {
            string result = string.Empty;

            if (!s.IsNullOrEmpty())
            {
                result = s;
                result = result.Replace(" ", "");
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that first ındex of. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">  The str to act on. </param>
        /// <param name="ch">   The ch. </param>
        ///
        /// <returns>   An int. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int FirstIndexOf(this string str, char ch)
        {
            int _index = -1;

            try
            {
                if (str.IsNullOrEmpty())
                    return _index;

                if (ch.IsNull())
                    return _index;

                char[] chs = str.ToCharArray();

                for (int charCounter = 0; charCounter < chs.Length; charCounter++)
                {
                    if (string.Format("{0}", ch).Equals(string.Format("{0}", chs[charCounter])))
                    {
                        _index = charCounter;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }

            return _index;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// A string extension method that removes the under line and capitalize string described by
        /// str removes underline and point.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">  The str to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string RemoveUnderLineAndCapitalizeString(this string str)
        {
            if (str.IsNullOrEmpty())
                return str;

            var result = str.Replace(" ", String.Empty);

            var s = str.Split(new char[] { '_', '.' }, StringSplitOptions.RemoveEmptyEntries) ?? new string[] { };

            s = s.Select(q => q.CapitalizeString()).ToArray();

            result = string.Join(string.Empty, s);

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that capitalize end part. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">      The str to act on. </param>
        /// <param name="endPart">  The end part. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string CapitalizeEndPart(this string str, string endPart)
        {
            if (str.IsNullOrEmpty())
                return str;

            if (endPart.IsNullOrEmpty())
                return str;

            var result = str;

            if (result.ToLowerInvariant().EndsWith(endPart.ToLowerInvariant()))
            {
                result = str.Substring(0, str.Length - endPart.Length);
                result = string.Concat(result, endPart.CapitalizeString());
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that capitalize string. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">  The str to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string CapitalizeString(this string str)
        {
            var s = str.ToLowerInvariant();

            s = string.Concat(s[0].ToString().ToUpperInvariant(), s.Substring(1));

            return s;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that removes the spaces described by str. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">  The str to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string RemoveSpaces(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            str = str.Replace(" ", string.Empty);
            return str;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that removes the characters. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">      The str to act on. </param>
        /// <param name="chars">    A variable-length parameters list containing characters. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string RemoveChars(this string str, params char[] chars)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (chars == null)
                return str;

            if (chars.Length < 1)
                return str;

            chars
                .ToList()
                .ForEach(q => str = str.Replace(q.ToString(), ""));

            return str;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that first character to upper. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="input">    The input to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string FirstCharToUpper(this string input)
        {
            if (input.IsNullOrSpace())
                return input;

            return input.First().ToString().ToUpperInvariant() + input.Substring(1);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that first character to lower. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="input">    The input to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string FirstCharToLower(this string input)
        {
            if (input.IsNullOrSpace())
                return input;

            return input.First().ToString().ToLowerInvariant() + input.Substring(1);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that uppercase first. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="s">    The s to act on. </param>
        ///
        /// <returns>   A string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string UppercaseFirst(this string s)
        {
            // Check for empty string.
            if (s.IsNullOrSpace())
                return s;

            // Return char and concat substring.
            return char.ToUpperInvariant(s[0]) + s.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static string RemovewithDictionary(this string str, Dictionary<string, string> dictionary)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            if (dictionary == null || dictionary.Count == 0)
                return str;

            var dds = dictionary.Where(q => !string.IsNullOrEmpty(q.Key) && q.Value != null).ToList();
            var s = str;

            foreach (var item in dds)
            {
                s = s.Replace(item.Key, item.Value);
            }

            return s;
        }

        /// <summary>
        /// The RemoveTurkishChars
        /// </summary>
        /// <param name="s">The s<see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string RemoveTurkishChars(this string s)
        {
            string strResult = string.Format("{0}", s);

            strResult = strResult.Replace("ğ", "g")
                .Replace("ı", "i").Replace("ç", "c")
                .Replace("ö", "o").Replace("ü", "u")
                .Replace("Ğ", "G").Replace("Ç", "C")
                .Replace("Ö", "O").Replace("Ü", "U")
                .Replace("İ", "I");

            return strResult;
        }
    }
}