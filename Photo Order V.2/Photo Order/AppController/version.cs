using Newtonsoft.Json;
using System.Net;

namespace AppController {
    public class Version {
        public Result result = new Result();
        public bool isConnected = false;
        public Version() {
            this.getResult();
        }
        private void getResult() {
            if (Network.isAvailableURL(AppLinks.Values.urlVersion)) {
                using (var wb = new WebClient()) {
                    var response = wb.DownloadString(AppLinks.Values.urlVersion);
                    this.result = JsonConvert.DeserializeObject<Result>(response);
                }
                this.isConnected = true;
            }
        }
        public class Result {
            public string app { get; set; }
            public string db { get; set; }
        }
    }
}
