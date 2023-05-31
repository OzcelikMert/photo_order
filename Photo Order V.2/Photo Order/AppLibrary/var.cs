using System;

namespace AppLibrary
{
    public class Var {
        public static bool isNullOrEmpty(params object[] args) {
            foreach (var arg in args)
            {
                if (arg == null || string.IsNullOrEmpty(arg.ToString()) || string.IsNullOrWhiteSpace(arg.ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        public static string toStringDateFormatDB(DateTime dateTime)
        {
            return (dateTime).ToString("yyyy-MM-dd");
        }

        public static string toStringDateFormatLabel(DateTime dateTime)
        {
            return (dateTime).ToString("dd/MM/yyyy");
        }

        public static string toStringDateFormatFolder(DateTime dateTime)
        {
            return (dateTime).ToString("dd.MM.yyyy");
        }

        public static string toStringDecimalFormat(double myNumber)
        {
            var s = string.Format("{0:0.00}", myNumber);
            if (s.EndsWith("00"))
            {
                return ((int)myNumber).ToString();
            }
            else
            {
                return s;
            }
        }
    }
}
