﻿using System;
using System.Windows;
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
                Age =StudAge.Text,
                ImgPath = ImagePath.Text
            });
            this.Close();
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
            ImagePath.Text = Directory.GetCurrentDirectory() + @"\Images\" + StudName.Text + StudAge.Text + ".jpg";
          
        }
    }
}
