namespace Mst.Dexter.Extensions.ObjectExtensions
{
    using System;
    using System.Threading;

    /// <summary>
    /// 
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets Day Name of CurrentCulture.
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static string GetDayName(this DayOfWeek dayOfWeek)
        {
            var result = Thread.CurrentThread.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek);
            return result;
        }

        /// <summary>
        /// Gets Day Name of given CurrentCulture.
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <param name="culture">culture name</param>
        /// <returns></returns>
        public static string GetDayName(this DayOfWeek dayOfWeek, string culture)
        {
            var cultureName = (culture ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(cultureName))
                cultureName = "tr-TR";

            var result = new System.Globalization.CultureInfo(cultureName).DateTimeFormat.GetDayName(dayOfWeek);
            return result;
        }
    }
}
