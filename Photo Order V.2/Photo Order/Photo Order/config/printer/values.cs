using System;

namespace Photo_Order.config.printer
{
    public class Values {
        public static string pathInvoice { get { return String.Format("{0}/invoice/", AppLibrary.Values.pathMainFolder); } }
        public static string pdfFileName { get { return "invoice.pdf"; } }
        public static string htmlFileName { get { return "invoice.html"; } }
    }
}
