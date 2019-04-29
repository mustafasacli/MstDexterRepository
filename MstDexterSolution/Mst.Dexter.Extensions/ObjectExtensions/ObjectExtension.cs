namespace Mst.Dexter.Extensions
{
    using System;

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>   An object extension. </summary>
    ///
    /// <remarks>   Msacli, 22.04.2019. </remarks>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    public static class ObjectExtension
    {
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that query if 'o' is null. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="o">    The o to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsNull(this object o)
        {
            return o == null;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// An object extension method that query if 'obj' is null or database null.
        /// </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsNullOrDbNull(this object obj)
        {
            return (null == obj || obj == DBNull.Value);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a string. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as a string. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static string ToStr(this object obj)
        {
            string str = string.Empty;

            try
            {
                str = obj.IsNullOrDbNull() ?
                    string.Empty : obj.ToString();
            }
            catch (Exception e)
            {
            }

            return str;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a int. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an int. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int ToInt(this object obj)
        {
            int result = 0;

            try
            {
                result = Convert.ToInt32(obj);
            }
            catch (Exception e)
            {
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a int nullable. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an int? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int? ToIntNullable(this object obj)
        {
            int? result = default(int?);

            if (obj.IsNullOrDbNull())
                return result;

            try
            {
                int a;

                if (int.TryParse(obj.ToString(), out a))
                    result = a;
            }
            catch (Exception e)
            {
                result = default(int?);
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a decimal. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as a decimal. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static decimal ToDecimal(this object obj)
        {
            decimal result = 0;

            try
            {
                result = Convert.ToDecimal(obj);
            }
            catch (Exception e)
            {
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A char extension method that character 2 int. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="ch">   The ch to act on. </param>
        ///
        /// <returns>   An int. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static int Char2Int(this char ch)
        {
            int i = 0;

            try
            {
                i = Convert.ToInt32(ch);
            }
            catch (Exception e)
            {
            }

            return i;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that query if 'str' is null or empty. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">  The str to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsNullOrEmpty(this string str)
        {
            if (str == null)
            {
                return true;
            }
            else
            {
                return str.Length == 0;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   A string extension method that query if 'str' is null or space. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="str">  The str to act on. </param>
        ///
        /// <returns>   True if it succeeds, false if it fails. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool IsNullOrSpace(this string str)
        {
            if (str != null)
            {
                return str.Replace(" ", "").Length == 0;
            }

            return true;
        }
    }
}