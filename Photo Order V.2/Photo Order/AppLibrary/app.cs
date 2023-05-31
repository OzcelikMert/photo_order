using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;

namespace AppLibrary
{
    public class App {
        public static string getProcessId() {
            string result = "";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select ProcessorID From Win32_processor")) {
                foreach (ManagementObject obj in searcher.Get()) {
                    result = obj["ProcessorID"].ToString();
                }
            }
            return result;
        }
        public static string getLocalIPv4(NetworkInterfaceType _type) {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        public static string getLanguageText(string key) => Application.Current.Resources[key] as string;
        public static Style getStyle(string key) => Application.Current.Resources[key] as Style;
    }
}
