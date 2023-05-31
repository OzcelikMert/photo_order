using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace Photo_Order_Customer.config
{
    public class RemotePaths {
        public Json json = new Json();
        public JsonFile jsonFile = new JsonFile();
        public RemotePaths() {
            if (!File.Exists(Values.pathRemotePathsFile)){
                this.jsonFile.ipAddress = "";
                string jsonString = JsonConvert.SerializeObject(this.jsonFile, Formatting.None);
                using (StreamWriter file = File.CreateText(Values.pathRemotePathsFile)) {
                    file.Write(jsonString);
                }
                this.getErrorMessage();
                return;
            }
            this.read();
        }
        private void getErrorMessage() {
            MessageBox.Show(String.Format("Please fill true paths '{0}'", Values.pathRemotePathsFile), "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
        private void read() {
            this.jsonFile = JsonConvert.DeserializeObject<JsonFile>(File.ReadAllText(Values.pathRemotePathsFile));
            if (AppLibrary.Var.isNullOrEmpty(this.jsonFile.ipAddress)) { getErrorMessage(); return; }
            this.json.pathPublicDocument = String.Format(@"\\{0}\Users\Public\Documents\PhotoOrder\", this.jsonFile.ipAddress);
            this.json.pathImageHigh = String.Format(@"\\{0}\PhotoOrder\", this.jsonFile.ipAddress);
            if (!Directory.Exists(this.json.pathPublicDocument)) {
                getErrorMessage();
            }
            if (!Directory.Exists(this.json.pathImageHigh)) {
                getErrorMessage();
            }
        }
        public class Json {
            public string pathImageHigh { get; set; }
            public string pathPublicDocument { get; set; }
        }
        public class JsonFile {
            public string ipAddress { get; set; }
        }
    }
}
