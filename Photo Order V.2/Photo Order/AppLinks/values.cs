using System;

namespace AppLinks
{
    public class Values {
        public static string domain { get { return "http://photoorder.xyz/app"; } }
        public static string urlVersion { get { return String.Format("{0}/version.json", domain); } }
        public static string urlLicense { get { return String.Format("{0}/license/", domain); } }
        public static string urlUpdaterInfo { get { return String.Format("{0}/updater/info.xml", domain); } }
        public static string urlUpdaterInfoCustomer { get { return String.Format("{0}/updater/infoCustomer.xml", domain); } }
    }
}
