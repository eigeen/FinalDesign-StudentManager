using StudentManager.Access;
using StudentManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
            this.DataContext = viewModelObj;

            js = new JsonAccess
            {
                SchoolPath = "_SchoolData.json",
                GradePath = "_GradeData.json"
            };
        }

        private MsgBoxAddStudentViewModel viewModelObj;
        private JsonAccess js;

        public string SelectedSchool { get; set; }
        public string SelectedMajor { get; set; }
        public string SelectedClass { get; set; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            var ls = viewModelObj.GetApplyData();
            js.UpdateGradeRoot(ls);

            var strls = new List<string> { };
            ls.ForEach(item => strls.Add(item.StuName));
            js.UpdateSchoolRoot(strls, "Student", SelectedSchool, SelectedMajor, SelectedClass);
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
