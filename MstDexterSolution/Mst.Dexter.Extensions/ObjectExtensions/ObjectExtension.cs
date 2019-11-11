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
            return (null == obj || obj == DBNull.Value || obj == (object)DBNull.Value);
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
        /// <summary>   An object extension method that converts an obj to a byte nullable. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an byte? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static byte? ToByteNullable(this object obj)
        {
            byte? result = default(byte?);

            if (obj.IsNullOrDbNull())
                return result;

            try
            {
                byte a;

                if (byte.TryParse(obj.ToString(), out a))
                    result = a;
            }
            catch (Exception e)
            {
                result = default(byte?);
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a short nullable. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an short? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static short? ToShortNullable(this object obj)
        {
            short? result = default(short?);

            if (obj.IsNullOrDbNull())
                return result;

            try
            {
                short a;

                if (short.TryParse(obj.ToString(), out a))
                    result = a;
            }
            catch (Exception e)
            {
                result = default(short?);
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
        /// <summary>   An object extension method that converts an obj to a long nullable. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an long? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static long? ToLongNullable(this object obj)
        {
            long? result = default(long?);

            if (obj.IsNullOrDbNull())
                return result;

            try
            {
                long a;

                if (long.TryParse(obj.ToString(), out a))
                    result = a;
            }
            catch (Exception e)
            {
                result = default(long?);
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a decimal nullable. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an decimal? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static decimal? ToDecimalNullable(this object obj)
        {
            decimal? result = default(decimal?);

            if (obj.IsNullOrDbNull())
                return result;

            try
            {
                decimal a;

                if (decimal.TryParse(obj.ToString(), out a))
                    result = a;
            }
            catch (Exception e)
            {
                result = default(decimal?);
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a double nullable. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an double? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static double? ToDoubleNullable(this object obj)
        {
            double? result = default(double?);

            if (obj.IsNullOrDbNull())
                return result;

            try
            {
                double a;

                if (double.TryParse(obj.ToString(), out a))
                    result = a;
            }
            catch (Exception e)
            {
                result = default(double?);
            }

            return result;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>   An object extension method that converts an obj to a float nullable. </summary>
        ///
        /// <remarks>   Msacli, 22.04.2019. </remarks>
        ///
        /// <param name="obj">  The obj to act on. </param>
        ///
        /// <returns>   Obj as an float? </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////
        public static float? ToFloatNullable(this object obj)
        {
            float? result = default(float?);

            if (obj.IsNullOrDbNull())
                return result;

            try
            {
                float a;

                if (float.TryParse(obj.ToString(), out a))
                    result = a;
            }
            catch (Exception e)
            {
                result = default(float?);
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

        /// <summary>
        /// if value is DbNulll.Value returns null, else return object value.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>Returns object</returns>
        public static object GetValueWithCheckNull(this object obj)
        {
            var value = obj == (object)DBNull.Value ? null : obj;
            return value;
        }

        /// <summary>
        /// Convert object to DateTime nullable.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime? ToDateTimeNullable(this object obj)
        {
            DateTime? val = null;
            DateTime dt;

            if (DateTime.TryParse(obj.ToStr(), out dt))
                val = dt;

            return val;
        }
    }
}