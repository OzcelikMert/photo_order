using System.Globalization;
using System.Threading;
using System.Windows;

namespace Photo_Order
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application
    {
        App() {
            string culture = "tr-TR";
            CultureInfo ci = new CultureInfo(culture);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            ci.DateTimeFormat.Calendar = new System.Globalization.GregorianCalendar();
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
        }
    }
}
