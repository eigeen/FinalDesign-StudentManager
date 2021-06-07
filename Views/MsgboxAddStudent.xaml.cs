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
            Visibility = Visibility.Hidden;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            List<Models.AddStudentModel> ls = viewModelObj.GetApplyData();
            js.UpdateGradeRoot(ls);

            List<string> strls = new List<string> { };
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
