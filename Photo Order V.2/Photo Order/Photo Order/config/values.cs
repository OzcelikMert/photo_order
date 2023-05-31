using System;
using System.Windows.Controls;

namespace Photo_Order.config
{
    public class Values {
        public static string pathUserDocumentFolder { get { return String.Format(@"{0}\PhotoOrder\", Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments));  } }
        public static string pathUploadsFolder { get { return String.Format(@"{0}\uploads\", pathUserDocumentFolder); } }
        public static string pathUploadsProductsFolder(long groupId = 0) => String.Format(@"{0}\products\{1}", pathUploadsFolder, (groupId > 0) ? String.Format(@"{0}\",groupId) : "");
        public static string pathUploadsProductOwnersFolder { get { return String.Format(@"{0}\productOwners\", pathUploadsFolder); } }
        public static string pathUploadsEventsFolder { get { return String.Format(@"{0}\events\", pathUploadsFolder); } }
        public static string pathUploadsBackgroundFolder { get { return String.Format(@"{0}\background\", pathUploadsFolder); } }
        public static string pathDesktopCopyImageFolder { get { return String.Format(@"{0}\PhotoOrder\", Environment.GetFolderPath(Environment.SpecialFolder.Desktop)); } }
        public static string pathImageHigh { get { return String.Format(@"\\{0}\PhotoOrder\", ipV4Address); } }
        public static string pathImagesFolder { get { return String.Format(@"{0}\images\", AppLibrary.Values.pathMainFolder); } }
        public static string ipV4Address = AppLibrary.App.getLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet);
        public static long selectedItemId = 0;
        public static bool refreshList = false;
        public static WindowMain.ComponentTabControl tabControl { get; set; }
        public static WindowEdit windowEdit { get; set; }
    }
}
