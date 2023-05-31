using System;

namespace AppLibrary
{
    public class Values {
        public static string pathMainFolder { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        public static Logger logger = new Logger();
        public static Settings settings { get; set; }
        public static db.DBInfo dbInfo { get; set; }
    }
}
