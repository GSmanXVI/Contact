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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd : Window
    {
        public StudentAdd()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StudentList.Add(new Student
            {
                Name = StudName.Text,
                Surname = StudSurname.Text,
                Age = Convert.ToInt32(StudAge.Text),
                ImgPath = ImagePath.Text
            });
            Close();
        }
    }
}
