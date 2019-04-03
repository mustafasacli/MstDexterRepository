using System;

namespace Mst.Dexter.PocoGenerator
{
    #region [ NameSpaceController static class ]

    internal static class NameSpaceController
    {
        public static bool ControlNameSpace(string nameSpace)
        {
            try
            {
                if (IsNullOrSpace(nameSpace) == false)
                {
                    char[] chStr = nameSpace.ToCharArray();
                    int ii = (int)chStr[0];

                    if (ii > 47 && ii < 58)
                        return false;

                    bool retBool = true;
                    foreach (var ch in chStr)
                    {
                        int i = (int)ch;
                        retBool &=
                            ((i > 47 && i < 58) ||
                             (i > 64 && i < 91) ||
                             (i > 96 && i < 123) ||
                             (i == 95) || (i == 46));
                    }
                    return retBool;
                }
                else
                    return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal static bool IsNullOrSpace(string value)
        {
            if (value == null)
                return true;
            else
                return value.Replace(" ", "").Length == 0;
        }
    }

    #endregion [ NameSpaceController static class ]
}