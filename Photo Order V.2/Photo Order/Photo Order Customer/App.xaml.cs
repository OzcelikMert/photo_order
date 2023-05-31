using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Photo_Order_Customer
{
    /// <summary>
    /// App.xaml etkileşim mantığı
    /// </summary>
    public partial class App : Application {
        public App() {}
        public static void SelectCulture(string culture) {
            if (String.IsNullOrEmpty(culture))
                return;

            //Copy all MergedDictionarys into a auxiliar list.
            var dictionaryList = Application.Current.Resources.MergedDictionaries.ToList();
            string pathLanguages = "/assets/languages/";
            //Search for the specified culture.     
            string requestedCulture = string.Format(pathLanguages + "{0}.xaml", culture);
            var resourceDictionary = dictionaryList.
                FirstOrDefault(d => d.Source.OriginalString == requestedCulture);

            if (resourceDictionary == null) {
                //If not found, select our default language.             
                requestedCulture = pathLanguages + "tr-TR.xaml";
                resourceDictionary = dictionaryList.
                    FirstOrDefault(d => d.Source.OriginalString == requestedCulture);
            }

            //If we have the requested resource, remove it from the list and place at the end.     
            //Then this language will be our string table to use.      
            if (resourceDictionary != null) {
                Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary);
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }

            //Inform the threads of the new culture.
            CultureInfo ci = new CultureInfo(culture);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            ci.DateTimeFormat.Calendar = new System.Globalization.GregorianCalendar();
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

        }
    }
}
