using System;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace TestTaskCadwise2.Models
{
    public class LanguageInfo
    {
        public static void ChangeLanguage()
        {
            var curLang = System.Threading.Thread.CurrentThread.CurrentUICulture;

            CultureInfo culture;
            ResourceDictionary dict = new ResourceDictionary();
            switch(curLang.Name)
            {
                case "ru-RU":
                    dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                    culture = new CultureInfo("en-US");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                    break;
                default:
                    dict.Source = new Uri("Resources/lang.ru-RU.xaml", UriKind.Relative);
                    culture = new CultureInfo("ru-RU");
                    System.Threading.Thread.CurrentThread.CurrentUICulture = culture;
                    break;
            }

            ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                          select d).First();
            if(oldDict != null)
            {
                int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
        }
    }
}
