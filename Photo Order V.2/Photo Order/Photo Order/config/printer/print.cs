using Spire.Pdf;
using Spire.Pdf.Print;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Printing;
using System.Windows;
using System.Windows.Controls;

namespace Photo_Order.config.printer
{
    public class Print {
        public Print() { }

        public bool printPdf() {
            bool isPrint = false;
            try {
                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(Values.pathInvoice + Values.pdfFileName);

                PrintDialog dialogPrint = new PrintDialog();

                PageRange pageRange = new PageRange();
                pageRange.PageFrom = 1;
                pageRange.PageTo = doc.Pages.Count;

                dialogPrint.PageRange = pageRange;
                dialogPrint.MinPage = 1;
                dialogPrint.MaxPage = (uint)doc.Pages.Count;
                dialogPrint.UserPageRangeEnabled = true;
                dialogPrint.PageRangeSelection = PageRangeSelection.AllPages;
                dialogPrint.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA5);

                if (dialogPrint.ShowDialog() == true) {
                    SizeF pageSize = doc.Pages[0].Size;
                    PaperSize paper = new PaperSize("Custom", (148 * 100), 210 * 100);
                    paper.RawKind = (int)PaperKind.Custom;

                    doc.PrintSettings.PaperSize = paper;
                    doc.PrintSettings.SelectPageRange(dialogPrint.PageRange.PageFrom, dialogPrint.PageRange.PageTo);
                    doc.PrintSettings.SelectSinglePageLayout(PdfSinglePageScalingMode.ActualSize, false);
                    doc.PrintSettings.SetPaperMargins(0, 0, 0, 0);
                    doc.PrintDocument.OriginAtMargins = true;
                    doc.PrintSettings.Landscape = false;
                    doc.PrintSettings.PrinterName = dialogPrint.PrintQueue.Name;
                    doc.PrintSettings.PrintController = new StandardPrintController();
                    doc.Print();
                    isPrint = true;
                }
                doc.Dispose();
            }
            catch (Exception ex) {
                MessageBox.Show("Sipariş için fiş çıkarılamadı." + ex.ToString(), "Yazdirma Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
                isPrint = false;
            }
            return isPrint;
        }
    }
}
