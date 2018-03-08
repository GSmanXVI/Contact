using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

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
            ExportFromXML();
        }
        private void SaveStudents()
        {
            using (StreamWriter reader = new StreamWriter(Directory.GetCurrentDirectory()))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(reader, StudentList.ToList());
            }
        }
        private void ExportFromXML()
        {
            using (StreamReader reader = new StreamReader(Directory.GetCurrentDirectory()))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(List<Student>));
                StudentList = (ObservableCollection<Student>)deserializer.Deserialize(reader);
                StudentlistBox.ItemsSource = StudentList;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd studentAdd = new StudentAdd();
            studentAdd.ShowDialog();
            StudentlistBox.ItemsSource = StudentList;
            SaveStudents();
        }
    }
}
