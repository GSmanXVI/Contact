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
        public static List<Student> StudentList;
        public MainWindow()
        {
            InitializeComponent();

            StudentList = new List<Student>();
            StudentList.Add(new Student
            {
                Name = "Tural",
                Surname = "Muradov",
                Age = 27,
                ImgPath = @"D:\Downloads\AddContact\WpfApp10\WpfApp10\Images\Img4946.jpg"
            });
            StudentlistBox.ItemsSource = StudentList;
            SaveStudents();
        }
        private void SaveStudents()
        {
            using (StreamWriter reader = new StreamWriter(@"D:\Downloads\AddContact\WpfApp10\WpfApp10\StudentsXML\Students.xml", true))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<Student>));
                serializer.Serialize(reader,StudentList);
            }
        }
    }
}
