﻿namespace Mst.Dexter.PocoGenerator.Source.Util
{
    using System;

    internal class ConvertUtil
    {
        public static int ToInt(string willBeConverted)
        {
            int returnValue = 0;
            try
            {
                int.TryParse(willBeConverted, out returnValue);
            }
            catch (Exception e)
            {
            }
            return returnValue;
        }
    }
}