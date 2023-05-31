using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;

namespace AppLibrary.db
{
    public class DBInfo {
        public Json json = new Json();
        public string connectionStringWithoutDBName { get { return String.Format(@"Server={0}; Port={1}; UID={2}; Password={3};",
                this.json.server,
                this.json.port,
                this.json.uid,
                this.json.password);
            }
        }
        public string dbName { get { return "photoorder";  } }
        public string connectionString { get { return String.Format(@"{0} Database={1};", this.connectionStringWithoutDBName, this.dbName); } }
        public string pathFolder { get { return String.Format(@"{0}\db\", this.path); } }
        public string pathInfo { get { return String.Format(@"{0}\info.json", this.pathFolder); } }
        private string path { get; set; }
        public DBInfo(string path, string server = "localhost", bool onlyRead = false) {
            this.path = path;
            if(!onlyRead) this.create(server);
            this.read();
            if(!onlyRead) this.refresh(server);

        }
        private void create(string server) {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            if (!Directory.Exists(this.pathFolder)) {
                Directory.CreateDirectory(this.pathFolder);
            }

            if (!System.IO.File.Exists(pathInfo)) {
                this.json.server = server;
                this.json.port = "3306";
                this.json.uid = "root";
                this.json.password = "";
                string jsonString = JsonConvert.SerializeObject(this.json, Formatting.None);
                using (StreamWriter file = System.IO.File.CreateText(pathInfo))
                {
                    file.Write(jsonString);
                }
                this.getErrorMessage();
                return;
            }
        }
        private void delete() {
            if (!Directory.Exists(path)) {
                if (!Directory.Exists(this.pathFolder)) {
                    if (System.IO.File.Exists(pathInfo)) {
                        System.IO.File.Delete(pathInfo);
                    }
                }
            }
        }
        private void refresh(string server) {
            if(this.json.server != server) {
                this.delete();
                this.create(server);
            }
        }
        private void getErrorMessage() {
            MessageBox.Show(String.Format("Please fill true paths '{0}'", pathInfo), "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
            Application.Current.Shutdown();
        }
        private void read() {
            this.json = JsonConvert.DeserializeObject<Json>(System.IO.File.ReadAllText(pathInfo));
        }
        public class Json {
            public string server { get; set; }
            public string port { get; set; }
            public string uid { get; set; }
            public string password { get; set; }
        }
    }
}
