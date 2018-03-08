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
using System.Windows.Shapes;

namespace WpfApp10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Student> StudentList = new List<Student>();
        public MainWindow()
        {
            InitializeComponent();

        }
        private void SaveStudents()
        {
            using (StreamWriter reader = new StreamWriter(@"D:\Downloads\AddContact\WpfApp10\WpfApp10\StudentsXML\Students.xml", true))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(reader,StudentList);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            StudentAdd studentAdd = new StudentAdd();
            studentAdd.ShowDialog();
            SaveStudents();
        }
    }
}
