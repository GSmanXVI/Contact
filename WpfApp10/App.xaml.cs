using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Globalization;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static List<CultureInfo> _Languages = new List<CultureInfo>();
        public static List<CultureInfo> Languages
        {
            get
            {
                return _Languages;
            }
        }

        public App()
        {
            InitializeComponent();
            _Languages.Clear();
            _Languages.Add(new CultureInfo("en-US"));
            _Languages.Add(new CultureInfo("ru-RU"));
        }

        public static event EventHandler LangChange;

        public static CultureInfo Lang
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("null");
                if (value == Thread.CurrentThread.CurrentUICulture) return;
                Thread.CurrentThread.CurrentUICulture = value;
                ResourceDictionary resDic = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU": resDic.Source = new Uri(String.Format("lang.{0}.xaml", value.Name), UriKind.Relative); break;
                    default:
                        resDic.Source = new Uri("lang.Default.xaml", UriKind.Relative);
                        break;
                }
                ResourceDictionary oldResDic = (from a in Application.Current.Resources.MergedDictionaries
                                                where a.Source != null && a.Source.OriginalString.StartsWith("lang.") select a ).First();
                if(oldResDic != null)
                {
                    int index = Application.Current.Resources.MergedDictionaries.IndexOf(oldResDic);
                    Application.Current.Resources.MergedDictionaries.Remove(oldResDic);
                    Application.Current.Resources.MergedDictionaries.Insert(index, resDic);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(resDic);
                }
                LangChange(Application.Current, new EventArgs());
            }
        }


    }
}
