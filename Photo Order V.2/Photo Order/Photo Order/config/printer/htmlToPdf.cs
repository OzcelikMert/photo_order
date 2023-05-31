using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using PdfSharp;
using PdfSharp.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Photo_Order.config.printer
{
    public class HtmlToPdf {
        private string htmlOrderProducts = "";
        private int totalCount = 0;
        private double totalPrice = 0;
        private string customerRoom { get; set; }
        private string customerName { get; set; }
        private string customerEmail { get; set; }
        private long invoiceNo { get; set; }
        private string paymentType { get; set; }
        private string date { get; set; }
        private string companyName { get; set; }
        public HtmlToPdf(string customerRoom, string customerName, string customerEmail, string paymentType, long invoiceNo, string date, string companyName) {
            this.customerRoom = customerRoom;
            this.customerName = customerName;
            this.customerEmail = customerEmail;
            this.paymentType = paymentType;
            this.invoiceNo = invoiceNo;
            this.date = date;
            this.companyName = companyName;
        }
        public void createPdf() {
            try {
                string htmlStyle = this.getHtmlStyle();
                string htmlBody = this.getHtmlBody();
                var html = String.Format(
                "<html lang='tr'>" +
                    "<head>" +
                        "<meta charset='UTF-8'>" +
                    "</head>" +
                    "<body>" +
                        "{0}" +
                    "</body>" +
                "</html>", htmlBody);

                if (!Directory.Exists(Values.pathInvoice))
                {
                    Directory.CreateDirectory(Values.pathInvoice);
                }

                using (FileStream fs = File.Create(Values.pathInvoice + Values.htmlFileName))
                {
                    Byte[] title = new UTF8Encoding(true).GetBytes("<style type='text/css'>" + htmlStyle + "</style>" + htmlBody);
                    fs.Write(title, 0, title.Length);
                }
                var cssData = PdfGenerator.ParseStyleSheet(htmlStyle, true);
                var config = new PdfGenerateConfig();
                config.PageSize = PageSize.A5;
                config.MarginLeft = 2;
                config.MarginRight = 2;
                config.PageOrientation = PageOrientation.Portrait;
                using (PdfDocument document = PdfGenerator.GeneratePdf(html, config, cssData, null)) {
                    //document.PageLayout = PdfPageLayout.SinglePage;
                    document.Save(Values.pathInvoice + Values.pdfFileName);
                    document.Close();
                }
            }
            catch (Exception exception) {
                MessageBox.Show("Sipariş PDF'e dönüştürülemediği için fiş çıkarılamadı." + exception.ToString(), "Dönüştürme Mesajı", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public void setOrderProducts(List<ListOrderProducts> listOrderProducts) {
            this.htmlOrderProducts = "";
            foreach (var orderProduct in listOrderProducts) {
                this.htmlOrderProducts += String.Format(
                    "<tr>" +
                        "<td align='left'>{0}</td>" +
                        "<td align='right'>{1}</td>" +
                        "<td align='right'>{2}</td>" +
                    "</tr>",
                    orderProduct.totalCount,
                    orderProduct.productOwnerName,
                    String.Format("({0}) {1} {2}", 
                        AppLibrary.Var.toStringDecimalFormat(orderProduct.price), 
                        AppLibrary.Var.toStringDecimalFormat(orderProduct.totalPrice), 
                        this.paymentType
                    )
                );
                this.totalPrice += orderProduct.totalPrice;
                this.totalCount += orderProduct.totalCount;
            }
        }
        private string getHtmlStyle() {
            return ".company-invoice { border-bottom: 0.5mm dotted black; }" +
                ".text-center { text-align: center; }" +
                "body { margin: 0; padding:0; }" +
                "h4 { font-size: 2mm; }" +
                "td { font-size: 2mm; }" +
                "th { font-size: 2mm; }";
        }
        private string getHtmlBody() {
            return "" +
                "<div class='company-invoice'>" +
                    "<table width='100%'>" +
                        "<tr>" +
                            "<th align='left'>" +
                                "<h4>Customer Room: " + this.customerRoom + "</h4>" +
                                "<h4>Customer Name: " + this.customerName + "</h4>" +
                                "<h4>Customer Email: " + this.customerEmail + "</h4>" +
                                "<h4>Photos Count: " + this.totalCount + "</h4>" +
                                "<h4>Total Price: " + AppLibrary.Var.toStringDecimalFormat(this.totalPrice) + " " + this.paymentType + "</h4>" +
                            "</th>" +
                            "<th align='right' style='vertical-align: text-top;'>" +
                                "<h4>Bill No: " + this.invoiceNo + "</h4>" +
                                "<h4>Date: " + this.date + "</h4>" +
                            "</th>" +
                        "</tr>" +
                    "</table>" +
                "</div>" +
                "<div>" +
                    "<table width='100%'>" +
                        "<tr>" +
                            "<th align='left'>" +
                                "<h4>Customer Room: " + this.customerRoom + "</h4>" +
                                "<h4>Customer Name: " + this.customerName + "</h4>" +
                                "<h4>Customer Email: " + this.customerEmail + "</h4>" +
                                "<h4>Photos Count: " + this.totalCount + "</h4>" +
                                "<h4>Total Price: " + AppLibrary.Var.toStringDecimalFormat(this.totalPrice) + " " + this.paymentType + "</h4>" +
                            "</th>" +
                            "<th align='right' style='vertical-align: text-top;'>" +
                                "<h4>Bill No: " + this.invoiceNo + "</h4>" +
                                "<h4>Date: " + this.date + "</h4>" +
                            "</th>" +
                        "</tr>" +
                    "</table>" +
                    "<div>" +
                        "<h4 class='text-center'>List of Photos</h4>" +
                        "<div>" +
                            "<table class='text-center' width='100%'>" +
                                "<tr>" +
                                    "<th align='left'>Count</th>" +
                                    "<th align='right'>Owner</th>" +
                                    "<th align='right'>Price</th>" +
                                "</tr>" +
                                this.htmlOrderProducts +
                            "</table>" +
                        "</div>" +
                    "</div>" +
                "</div>";
        }
        public class ListOrderProducts {
            public long productOwnerId { get; set; }
            public int totalCount { get; set; }
            public string productOwnerName { get; set; }
            public double price { get; set; }
            public double totalPrice { get; set; }
        }
    }
}
