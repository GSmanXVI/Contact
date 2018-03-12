using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Threading;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media.Animation;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableCollection<Student> StudentList = new ObservableCollection<Student>();
        public MainWindow()
        {
            InitializeComponent();
          
        }
        
        private void SaveStudents()
        {
            using (StreamWriter writer = new StreamWriter("StudentList.xml"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(writer, StudentList.ToList());
            }
        }
        private bool ExportFromXML()
        {
            if (!File.Exists("StudentList.xml"))
            {
                return false;
            }
            
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\StudentList.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Student>));
                StudentList = (ObservableCollection<Student>)deserializer.Deserialize(reader);
                StudentlistBox.ItemsSource = StudentList;
                countLabel.Content = StudentList.Count;
               // StudInfobox.ItemsSource = StudentList;
                return true;
            }
        }

        private void AddMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd studentAdd = new StudentAdd();
            studentAdd.ShowDialog();
            StudentlistBox.ItemsSource = StudentList;
            //StudInfobox.ItemsSource = StudentList;
            countLabel.Content = StudentList.Count;
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            progresBar.Visibility = Visibility.Visible;
            Duration duration = new Duration(TimeSpan.FromSeconds(2));
            DoubleAnimation animation = new DoubleAnimation(200.0, duration);
            progresBar.BeginAnimation(ProgressBar.ValueProperty, animation);
            Thread.Sleep(1000);
            SaveStudents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ThemBox.Items.Add("Theme");
            ThemBox.Items.Add("DarkBlack");


            if (ExportFromXML())
            {
                Duration duration = new Duration(TimeSpan.FromSeconds(3));
                DoubleAnimation animation = new DoubleAnimation(200.0, duration);
                progresBar.BeginAnimation(ProgressBar.ValueProperty, animation);
            }
            else 
            progresBar.Visibility = Visibility.Hidden;
        }
       
       

        private void DeleteMenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            StudentList.Remove((Student)StudentlistBox.SelectedItem);
        }

        private void OpenMenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe","StudentList.xml");
        }

        private void StudInfobox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
           
        }

        private void ThemBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string them = ThemBox.SelectedItem as string;
            var uri = new Uri(them + ".xaml", UriKind.Relative);
            ResourceDictionary resource = Application.LoadComponent(uri) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource);
        }
    }
}
