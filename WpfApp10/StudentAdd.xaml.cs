using System.Windows;
using System.IO;
using System.Windows.Forms;

namespace WpfApp10
{
    /// <summary>
    /// Логика взаимодействия для StudentAdd.xaml
    /// </summary>
    public partial class StudentAdd 
    {
        public StudentAdd()
        {
            InitializeComponent();
        }
        
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.StudentList.Add(new Student
            {
                StudentName = StudName.Text,
                Surname = StudSurname.Text,
                Age =StudAge.Text,
                ImgPath = ImagePath.Text
            });
          Close();
        }
       
        private void BrowserBtn_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog fileDialog = new OpenFileDialog();
            if(fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
              
                if (!File.Exists(Directory.GetCurrentDirectory() + @"\Images\" + StudName.Text + StudAge.Text + ".jpg"))
                {
                    File.Copy(fileDialog.FileName, Directory.GetCurrentDirectory() + @"\Images\" +  StudName.Text + StudAge.Text + ".jpg");
                }
                else 
                {
                   System.Windows.MessageBox.Show("This name already exists!");
                    return;
                }
            } else { return; }
            ImagePath.Text = Directory.GetCurrentDirectory() + @"\Images\" + StudName.Text + StudSurname.Text + StudAge.Text + ".jpg";
          
        }
    }

   
}
