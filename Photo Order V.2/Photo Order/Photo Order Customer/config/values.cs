using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Photo_Order_Customer.config
{
    public class Values {
        public static string pathImagesFolder { get { return String.Format("{0}/images/", AppLibrary.Values.pathMainFolder); } }
        private static string pathUploadsFolder { get { return String.Format("{0}/uploads/", remotePaths.json.pathPublicDocument); } }
        public static string pathUploadsProductsFolder(long groupId) => String.Format(@"{0}\products\{1}\", pathUploadsFolder, groupId);
        public static string pathUploadsProductOwnersFolder { get { return String.Format(@"{0}\productOwners\", pathUploadsFolder); } }
        public static string pathUploadsEventsFolder { get { return String.Format(@"{0}\events\", pathUploadsFolder); } }
        public static string pathUploadsBackgroundFolder { get { return String.Format(@"{0}\background\", pathUploadsFolder); } }
        public static string pathRemotePathsFile { get { return String.Format("{0}/remotePaths.json", AppLibrary.Values.pathMainFolder); } }
        public static WindowMain.ComponentTabControl tabControl { get; set; }
        public static long customerId = 0;
        public static RemotePaths remotePaths { get; set; }
        public static List<db.functions.Select.ListBasket> listBasket { get; set; }
    }
}
