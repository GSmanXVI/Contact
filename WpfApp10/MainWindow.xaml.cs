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
        private void ExportFromXML()
        {
            if (!File.Exists("StudentList.xml"))
            {
                return;
            }
            
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory() + @"\StudentList.xml"))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(ObservableCollection<Student>));
                StudentList = (ObservableCollection<Student>)deserializer.Deserialize(reader);
                StudentlistBox.ItemsSource = StudentList;
                countLabel.Content = StudentList.Count;
                StudInfobox.ItemsSource = StudentList;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd studentAdd = new StudentAdd();
            studentAdd.ShowDialog();
            StudentlistBox.ItemsSource = StudentList;
            StudInfobox.ItemsSource = StudentList;
            countLabel.Content = StudentList.Count;
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SaveStudents();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            progresBar.Value = 30;
            Thread.Sleep(1000);
            progresBar.Value += 30;
            Thread.Sleep(1000);
            progresBar.Value += 40;
            ExportFromXML();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StudInfobox.Visibility = Visibility.Visible;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
           
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Process.Start("notepad.exe","StudentList.xml");
        }
    }
}
