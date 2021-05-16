using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using StudentManager.Access;
using StudentManager.Models;
using StudentManager.Views;

namespace StudentManager.ViewModels
{
    class ManagePageViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public ManagePageViewModel()
        {
            js.SchoolPath = "_SchoolData.json";
            js.GradePath = "_GradeData.json";
            schoolRoot = js.SchoolLoad();
            gradeRoot = js.GradeLoad();

            LoadComboBoxSchool();
            DataGridSource = new ObservableCollection<CoursesItem> { };

        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            System.Environment.Exit(0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<CoursesItem> dataGridSource;

        public ObservableCollection<CoursesItem> DataGridSource
        {
            get { return dataGridSource; }
            set
            {
                dataGridSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataGridSource"));
            }
        }

        private ObservableCollection<ComboBoxElement> comboSchools;

        public ObservableCollection<ComboBoxElement> ComboSchools
        {
            get { return comboSchools; }
            set
            {
                comboSchools = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComboSchools"));
            }
        }

        private ObservableCollection<ComboBoxElement> comboMajors;

        public ObservableCollection<ComboBoxElement> ComboMajors
        {
            get { return comboMajors; }
            set
            {
                comboMajors = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComboMajors"));
            }
        }

        private ObservableCollection<ComboBoxElement> comboClasses;

        public ObservableCollection<ComboBoxElement> ComboClasses
        {
            get { return comboClasses; }
            set
            {
                comboClasses = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComboClasses"));
            }
        }


        private string selectedSchool;

        public string SelectedSchool
        {
            get { return selectedSchool; }
            set
            {
                selectedSchool = value;
                SelectedSchoolObj = schoolRoot.Schools.Find(e => e.Name == selectedSchool);
            }
        }

        private string selectedMajor;

        public string SelectedMajor
        {
            get { return selectedMajor; }
            set
            {
                selectedMajor = value;
                SelectedMajorObj = SelectedSchoolObj.Majors.Find(e => e.Name == selectedMajor);
            }
        }



        private string selectedClass;

        public string SelectedClass
        {
            get { return selectedClass; }
            set
            {
                selectedClass = value;
                SelectedClassObj = SelectedMajorObj.Classes.Find(e => e.Name == selectedClass);
            }
        }

        private SchoolsItem selectedSchoolObj;

        public SchoolsItem SelectedSchoolObj
        {
            get { return selectedSchoolObj; }
            set
            {
                selectedSchoolObj = value;
            }
        }



        private MajorsItem selectedMajorObj;

        public MajorsItem SelectedMajorObj
        {
            get { return selectedMajorObj; }
            set { selectedMajorObj = value; }
        }

        private ClassesItem selectedClassObj;

        public ClassesItem SelectedClassObj
        {
            get { return selectedClassObj; }
            set { selectedClassObj = value; }
        }

        private ObservableCollection<ListBoxElement> listBoxStudent;



        public ObservableCollection<ListBoxElement> ListBoxStudents
        {
            get { return listBoxStudent; }
            set
            {
                listBoxStudent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListBoxStudents"));
            }
        }

        private string selectedStudent;

        public string SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                selectedStudent = value;
                var stu = selectedClassObj.Students.Find(e => e.Name == selectedStudent);
                SelectedStudentObj = gradeRoot.Grades.Find(e => e.ID == stu.ID);
            }
        }


        private GradesItem selectedStudentObj;

        public GradesItem SelectedStudentObj
        {
            get { return selectedStudentObj; }
            set { selectedStudentObj = value; }
        }

        private JsonAccess js = new JsonAccess();
        private SchoolRoot schoolRoot;
        private GradeRoot gradeRoot;


        public int DataGridSelectedIdx { get; set; }
        /// <summary>
        /// 获取学院列表
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<string> GetSchoolsList()
        {
            var names = new List<string> { };
            if (schoolRoot is null)
            {
                return names;
            }
            schoolRoot.Schools.ForEach(item => { names.Add(item.Name); });
            return names;
        }
        /// <summary>
        /// 获取学院-学科列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetMajorsList()
        {
            var majors = new List<string> { };
            if (selectedSchoolObj is null)
            {
                return majors;
            }
            SelectedSchoolObj.Majors.ForEach(item => { majors.Add(item.Name); });
            return majors;
        }
        /// <summary>
        /// 获取学科-班级列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetClassesList()
        {
            var classes = new List<string> { };
            if (SelectedMajorObj is null)
            {
                return classes;
            }
            SelectedMajorObj.Classes.ForEach(item => classes.Add(item.Name));
            return classes;
        }
        /// <summary>
        /// 获得班级-学生列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetStudentsList()
        {
            var students = new List<string> { };
            if (selectedClassObj.Students is null)
            {
                return students;
            }
            selectedClassObj.Students.ForEach(item => students.Add(item.Name));
            return students;
        }

        /// <summary>
        /// 载入学院列表
        /// </summary>
        public void LoadComboBoxSchool()
        {
            var schoolsList = GetSchoolsList();
            ComboSchools = new ObservableCollection<ComboBoxElement> { };
            schoolsList.ForEach(item => { ComboSchools.Add(new ComboBoxElement { Text = item }); });
            ComboSchools.Add(new ComboBoxElement { Text = "--添加--" });

        }
        /// <summary>
        /// 载入专业列表
        /// </summary>
        public void LoadComboBoxMajor()
        {
            var majorsList = GetMajorsList();
            ComboMajors = new ObservableCollection<ComboBoxElement> { };
            majorsList.ForEach(item => { ComboMajors.Add(new ComboBoxElement { Text = item }); });
            ComboMajors.Add(new ComboBoxElement { Text = "--添加--" });
        }
        /// <summary>
        /// 载入班级列表
        /// </summary>
        public void LoadComboBoxClass()
        {
            var classesList = GetClassesList();
            ComboClasses = new ObservableCollection<ComboBoxElement> { };
            classesList.ForEach(item => { ComboClasses.Add(new ComboBoxElement { Text = item }); });
            ComboClasses.Add(new ComboBoxElement { Text = "--添加--" });
        }
        /// <summary>
        /// 载入学生列表
        /// </summary>
        public void LoadListBoxStudent()
        {
            var studentList = GetStudentsList();
            ListBoxStudents = new ObservableCollection<ListBoxElement> { };
            studentList.ForEach(item => { ListBoxStudents.Add(new ListBoxElement { Text = item }); });
        }
        /// <summary>
        /// 载入学生所有成绩
        /// </summary>
        /// <param name="selectedStudent"></param>
        public void LoadDataGrid(string selectedStudent)
        {
            SelectedStudent = selectedStudent;
            if (SelectedStudentObj is null)
            {
                DataGridSource = new ObservableCollection<CoursesItem> { };
                return;
            }
            DataGridSource = new ObservableCollection<CoursesItem> { };
            var studentID = SelectedStudentObj.ID;
            gradeRoot.Grades.Find(e => e.ID == studentID).Courses.ForEach(item => { DataGridSource.Add(item); });
        }
        /// <summary>
        /// 应用表格内的修改到文件
        /// </summary>
        /// <param name="coursesItems"></param>
        public void UpdateData(ObservableCollection<CoursesItem> coursesItems)
        {
            var ls = new List<CoursesItem> { };
            foreach (var item in coursesItems)
            {
                ls.Add(item);
            }
            var studentID = SelectedStudentObj.ID;
            js.UpdateGrade(ls, studentID);
        }

        public void RefreshComboBox()
        {
            schoolRoot = js.SchoolLoad();
        }
        public void AddComboBoxSchool()
        {
            MessageBoxAddItems msgBox = new MessageBoxAddItems();
            msgBox.ShowDialog();
        }
        public void AddComboBoxMajor()
        {
            throw new NotImplementedException();
        }
        public void AddComboBoxClass()
        {
            throw new NotImplementedException();
        }
    }
}

