using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Globalization;

namespace WpfApp10
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App 
    {
        public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();

        public App()
        {
            InitializeComponent();
            Languages.Clear();
            Languages.Add(new CultureInfo("en-US"));
            Languages.Add(new CultureInfo("ru-RU"));
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
                if (value == null) throw new ArgumentNullException();
                if (value.Equals(Thread.CurrentThread.CurrentUICulture)) return;
                Thread.CurrentThread.CurrentUICulture = value;
                ResourceDictionary resDic = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru-RU": resDic.Source = new Uri(String.Format("lang.{0}.xaml", value.Name), UriKind.Relative); break;
                    default:
                        resDic.Source = new Uri("lang.Default.xaml", UriKind.Relative);
                        break;
                }
                ResourceDictionary oldResDic = (from a in Current.Resources.MergedDictionaries
                                                where a.Source != null && a.Source.OriginalString.StartsWith("lang.") select a ).First();
                if(oldResDic != null)
                {
                    int index = Current.Resources.MergedDictionaries.IndexOf(oldResDic);
                    Current.Resources.MergedDictionaries.Remove(oldResDic);
                    Current.Resources.MergedDictionaries.Insert(index, resDic);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(resDic);
                }

                LangChange?.Invoke(Current, new EventArgs());
            }
        }


    }
}
