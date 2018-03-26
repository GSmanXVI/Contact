using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media.Animation;
using System.Globalization;
using System.Windows.Controls.Primitives;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static ObservableCollection<Student> StudentList = new ObservableCollection<Student>();
        public MainWindow()
        {
            InitializeComponent();

            App.LangChange += App_LangChange;

            CultureInfo curLang = App.Lang;

            LangMenu.Items.Clear();
            foreach (var lang in App.Languages)
            {
                MenuItem menuItem = new MenuItem();
                menuItem.Header= lang.DisplayName;
                menuItem.Tag = lang;
                menuItem.IsChecked = lang.Equals(curLang);
                menuItem.Click += MenuItem_Click;
                LangMenu.Items.Add(menuItem);
            }
        }
        StudentAdd studentAdd = new StudentAdd();
        private void App_LangChange(object sender, EventArgs e)
        {
            CultureInfo currLang = App.Lang;

            foreach (MenuItem i in LangMenu.Items)
            {
                CultureInfo culture = i.Tag as CultureInfo;
                i.IsChecked = culture != null && culture.Equals(currLang);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem menuItem)
            {
                CultureInfo lang = menuItem.Tag as CultureInfo;
                if (lang != null)
                {
                    App.Lang = lang;
                }
            }
        }

       

        private void SaveStudents()
        {
            using (StreamWriter writer = new StreamWriter("StudentList.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(writer, StudentList.ToList());
            }
        }
        private bool ExportFromXml()
        {
            if (!File.Exists("StudentList.xml"))
            {
                Directory.CreateDirectory("Images");
                return false;
            }
            
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\StudentList.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Student>));
                StudentList = (ObservableCollection<Student>)deserializer.Deserialize(reader);
                StudentlistBox.ItemsSource = StudentList;
                CountLabel.Content = StudentList.Count;
                return true;
            }
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            studentAdd = new StudentAdd();
            studentAdd.UpdateBtn.IsEnabled = false;
            studentAdd.SaveBtn.IsEnabled = true;
            studentAdd.ShowDialog();
            StudentlistBox.ItemsSource = StudentList;
            CountLabel.Content = StudentList.Count;
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            progresBar.Visibility = Visibility.Visible;
            Duration duration = new Duration(TimeSpan.FromSeconds(2));
            DoubleAnimation animation = new DoubleAnimation(200.0, duration);
            progresBar.BeginAnimation(RangeBase.ValueProperty, animation);
            Thread.Sleep(1000);
            SaveStudents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThemBox.Items.Add("Theme");
            ThemBox.Items.Add("DarkBlack");


            if (ExportFromXml())
            {
                Duration duration = new Duration(TimeSpan.FromSeconds(3));
                DoubleAnimation animation = new DoubleAnimation(200.0, duration);
                progresBar.BeginAnimation(RangeBase.ValueProperty, animation);
            }
            else 
            progresBar.Visibility = Visibility.Hidden;
        }
       
       

        private void DeleteMenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            
            StudentList.Remove((Student)StudentlistBox.SelectedItem);
            CountLabel.Content = StudentList.Count;
        }

        private void OpenMenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe","StudentList.xml");
        }

        private void ThemBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string them = ThemBox.SelectedItem as string;
            var uri = new Uri(them + ".xaml", UriKind.Relative);
            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StudentlistBox.SelectedItem != null)
            {
                studentAdd = new StudentAdd();
                studentAdd.SaveBtn.IsEnabled = false;
                studentAdd.UpdateBtn.IsEnabled = true;
                studentAdd.ShowDialog();
            }
            else
            {
                MessageBox.Show("You not selected item!");
                return;
            }
        }
    }
}
