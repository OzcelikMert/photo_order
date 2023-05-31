using System;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Windows;

namespace AppController {
    public class Network {
        public Network() { }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);
        public static bool isThere() {
            int description;
            return InternetGetConnectedState(out description, 0);
        }
        public static bool isAvailableURL(string url) {
            string Message = string.Empty;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            // Set the credentials to the current user account
            request.Credentials = System.Net.CredentialCache.DefaultCredentials;
            request.Timeout = 10000;
            request.ReadWriteTimeout = 10000;
            request.Method = "GET";

            try {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) {
                    // Do nothing; we're only testing to see if we can get the response
                    
                }
            } catch (WebException ex) {
                Message += ((Message.Length > 0) ? "\n" : "") + ex.Message;
            }

            return (Message.Length == 0);
        }
    }
}
