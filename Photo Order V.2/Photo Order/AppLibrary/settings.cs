using Newtonsoft.Json;
using System;
using System.IO;

namespace AppLibrary
{
    public class Settings {
        public Json json = new Json();
        private string path { get; set; }
        private string pathFile { get { return String.Format("{0}/settings.json", this.path); } }
        public Settings(string pathFolder) {
            this.path = pathFolder;
            this.create();
            this.read();
        }
        private void create(bool getJsonData = false) {
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            if (!System.IO.File.Exists(this.pathFile)) {
                if (!getJsonData) {
                    this.json.companyName = "";
                    this.json.backgroundLogo = "";
                    this.json.priceTurkishLira = 0;
                    this.json.priceDollar = 0;
                    this.json.priceEuro = 0;
                    this.json.pricePound = 0;
                }
                string jsonString = JsonConvert.SerializeObject(json, Formatting.None);
                using (StreamWriter file = System.IO.File.CreateText(this.pathFile)) {
                    file.Write(jsonString);
                }
            }
        }
        public void write() {
            if (System.IO.File.Exists(this.pathFile)) {
                System.IO.File.Delete(this.pathFile);
            }
            this.create(true);
        }
        private void read() {
            this.json = JsonConvert.DeserializeObject<Json>(System.IO.File.ReadAllText(this.pathFile));
        }
        public class Json {
            public string companyName { get; set; }
            public string backgroundLogo { get; set; }
            public double priceTurkishLira { get; set; }
            public double priceDollar { get; set; }
            public double priceEuro { get; set; }
            public double pricePound { get; set; }
        }
    }
}
