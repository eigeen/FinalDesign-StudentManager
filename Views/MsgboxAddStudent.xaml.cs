using StudentManager.Access;
using StudentManager.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace StudentManager.Views
{
    /// <summary>
    /// MsgboxAddStudent.xaml 的交互逻辑
    /// </summary>
    public partial class MsgBoxAddStudent : Window
    {
        public MsgBoxAddStudent()
        {
            InitializeComponent();
            viewModelObj = new MsgBoxAddStudentViewModel();
            DataContext = viewModelObj;
        }

        private MsgBoxAddStudentViewModel viewModelObj;

        public string SelectedSchool { get; set; }
        public string SelectedMajor { get; set; }
        public string SelectedClass { get; set; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            StuDataGrid.CanUserAddRows = false;
            viewModelObj.Apply(SelectedClass);
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnDelRow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
