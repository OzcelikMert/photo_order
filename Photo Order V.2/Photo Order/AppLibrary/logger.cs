using System;
using System.IO;

namespace AppLibrary
{
    public class Logger {
        string path { get { return String.Format("{0}/logs/", Values.pathMainFolder); } }
        public Logger() {
            this.create(String.Format("Login Date {0}", DateTime.Now));
        }
        private void create(string log) {
            string fileName = String.Format("{0}.txt", DateTime.Now.ToString("dd-MM-yyyy"));
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }

            if(!System.IO.File.Exists(path + fileName)) {
                using (FileStream fileStream = System.IO.File.Create(path + fileName)) { }
            }

            using (StreamWriter file = new StreamWriter(path + fileName, append: true)) {
                file.WriteLine(log);
                file.WriteLine("---------------------------------------------------");
            }
        }
        public void write(string log) {
            log = String.Format("Log Time '{0}': {1}", DateTime.Now, log);
            this.create(log);
        }
        public delegate void Func();
        public void loggerFunction(Func func) {
            try { func(); }
            catch (Exception ex) {
                this.write(String.Format("Message: {0}\nSource: {1}\nStackTrace: {2}", ex.Message, ex.Source, ex.StackTrace));
            }
        }
    }
}
