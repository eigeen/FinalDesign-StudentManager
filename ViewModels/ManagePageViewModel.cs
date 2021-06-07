using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using StudentManager.Access;
using StudentManager.Models;
using StudentManager.Views;

namespace StudentManager.ViewModels
{
    public class ManagePageViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public ManagePageViewModel()
        {
            //js.InitDB();
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
                StudentsItem stu = selectedClassObj.Students.Find(e => e.Name == selectedStudent);
                selectedStudentObj = gradeRoot.Grades.Find(e => e.Name == stu.Name);
                if (selectedStudentObj == null) { SelectedStudentObj = new GradesItem { }; }
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
            List<string> names = new List<string> { };
            schoolRoot?.Schools?.ForEach(item => { names.Add(item.Name); });
            return names;
        }
        /// <summary>
        /// 获取学院-学科列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetMajorsList()
        {
            List<string> majors = new List<string> { };
            SelectedSchoolObj?.Majors?.ForEach(item => { majors.Add(item.Name); });
            return majors;
        }
        /// <summary>
        /// 获取学科-班级列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetClassesList()
        {
            List<string> classes = new List<string> { };
            SelectedMajorObj?.Classes?.ForEach(item => classes.Add(item.Name));
            return classes;
        }
        /// <summary>
        /// 获得班级-学生列表
        /// </summary>
        /// <returns></returns>
        private List<string> GetStudentsList()
        {
            List<string> students = new List<string> { };
            selectedClassObj?.Students?.ForEach(item => students.Add(item.Name));
            return students;
        }

        /// <summary>
        /// 载入学院列表
        /// </summary>
        public void LoadComboBoxSchool()
        {
            List<string> schoolsList = GetSchoolsList();
            ComboSchools = new ObservableCollection<ComboBoxElement> { };
            schoolsList.ForEach(item => { ComboSchools.Add(new ComboBoxElement { Text = item }); });
            ComboSchools.Add(new ComboBoxElement { Text = "--添加--" });

        }
        /// <summary>
        /// 载入专业列表
        /// </summary>
        public void LoadComboBoxMajor()
        {
            List<string> majorsList = GetMajorsList();
            ComboMajors = new ObservableCollection<ComboBoxElement> { };
            majorsList.ForEach(item => { ComboMajors.Add(new ComboBoxElement { Text = item }); });
            ComboMajors.Add(new ComboBoxElement { Text = "--添加--" });
        }
        /// <summary>
        /// 载入班级列表
        /// </summary>
        public void LoadComboBoxClass()
        {
            List<string> classesList = GetClassesList();
            ComboClasses = new ObservableCollection<ComboBoxElement> { };
            classesList.ForEach(item => { ComboClasses.Add(new ComboBoxElement { Text = item }); });
            ComboClasses.Add(new ComboBoxElement { Text = "--添加--" });
        }
        /// <summary>
        /// 载入学生列表
        /// </summary>
        public void LoadListBoxStudent()
        {
            List<string> studentList = GetStudentsList();
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
            if (SelectedStudentObj.Courses is null)
            {
                DataGridSource = new ObservableCollection<CoursesItem> { };
                return;
            }
            DataGridSource = new ObservableCollection<CoursesItem> { };
            string studentID = SelectedStudentObj.ID;
            gradeRoot.Grades.Find(e => e.ID == studentID).Courses.ForEach(item => { DataGridSource.Add(item); });
        }
        /// <summary>
        /// 应用表格内的修改到文件
        /// </summary>
        /// <param name="coursesItems"></param>
        public void UpdateData(ObservableCollection<CoursesItem> coursesItems)
        {
            List<CoursesItem> ls = new List<CoursesItem> { };
            foreach (CoursesItem item in coursesItems)
            {
                ls.Add(item);
            }
            string studentID = SelectedStudentObj.ID;
            js.UpdateGrade(ls, studentID);
        }

        public void RefreshSelectionBox()
        {
            schoolRoot = js.SchoolLoad();
            if (SelectedSchool != null) { SelectedSchool = SelectedSchool; }
            if (SelectedMajor != null) { SelectedMajor = SelectedMajor; }
            if (SelectedClass != null) { SelectedClass = SelectedClass; }
        }

        public void AddComboBoxSchool()
        {
            MsgBoxAddItems msgBox = new MsgBoxAddItems
            {
                ApplyObj = "School"
            };
            msgBox.ShowDialog();
        }
        public void AddComboBoxMajor()
        {
            MsgBoxAddItems msgBox = new MsgBoxAddItems
            {
                ApplyObj = "Major",
                SelectedSchool = SelectedSchool
            };
            msgBox.ShowDialog();
        }
        public void AddComboBoxClass()
        {
            MsgBoxAddItems msgBox = new MsgBoxAddItems
            {
                ApplyObj = "Class",
                SelectedSchool = SelectedSchool,
                SelectedMajor = SelectedMajor
            };
            msgBox.ShowDialog();
        }

        public void AddListBoxStudent()
        {
            if (SelectedClassObj == null) { SelectedClassObj = new ClassesItem { }; }
            MsgBoxAddStudent msgBox = new MsgBoxAddStudent
            {
                SelectedSchool = SelectedSchool,
                SelectedMajor = SelectedMajor,
                SelectedClass = SelectedClass
            };
            msgBox.ShowDialog();
        }
    }
}

