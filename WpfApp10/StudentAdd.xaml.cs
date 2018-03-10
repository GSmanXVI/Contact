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
using System.IO;
using System.Windows.Forms;

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
            this.Close();
        }

        private void BrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
               string path = fileDialog.FileName;
                // string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\Images\" + StudName.Text + StudAge.Text + ".jpg"))
                {
                    File.Copy(path, Directory.GetCurrentDirectory() + @"\Images\" +  StudName.Text + StudAge.Text + ".jpg");
                }
                else
                {
                   System.Windows.MessageBox.Show("This name already exists!");
                    return;
                }
            }
            ImagePath.Text = Directory.GetCurrentDirectory() + @"\Images\" + StudName.Text + StudAge.Text + ".jpg";
        }
    }
}
