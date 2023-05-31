using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.Collections.Specialized;
using System.Windows;

namespace AppController {
    public class License {
        public Result result = new Result();
        public bool isConnected = false;
        public License(string serialKey) {
            this.getResult(serialKey);
        }
        private void getResult(string serialKey) {
            if (Network.isAvailableURL(AppLinks.Values.urlLicense)) {
                using (var wb = new WebClient()) {
                    var data = new NameValueCollection();
                    data["ownerSerialKey"] = serialKey;
                    var response = wb.UploadValues(AppLinks.Values.urlLicense, "POST", data);
                    string responseString = Encoding.UTF8.GetString(response);
                    this.result = JsonConvert.DeserializeObject<Result>(responseString);
                }
                this.isConnected = true;
            }

        }
        public class ErrorCodes {
            public static readonly int Success = 1;
            public static readonly int WrongLicenseCode = 2;
        }
        public class Result {
            public bool status { get; set; }
            public string message { get; set; }
            public int errorCode { get; set; }
            public string licenseDateEnd { get; set; }
        }
    }
}
