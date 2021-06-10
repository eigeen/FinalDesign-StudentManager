using System;
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
            db.Connect("data.db");

            LoadComboBoxSchool();
            DataGridSource = new ObservableCollection<GradeObject> { };

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private readonly SqliteAccess db = new SqliteAccess();

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<GradeObject> dataGridSource;

        public ObservableCollection<GradeObject> DataGridSource
        {
            get => dataGridSource;
            set
            {
                dataGridSource = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DataGridSource"));
            }
        }

        private ObservableCollection<ComboBoxElement> comboSchools;

        public ObservableCollection<ComboBoxElement> ComboSchools
        {
            get => comboSchools;
            set
            {
                comboSchools = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComboSchools"));
            }
        }

        private ObservableCollection<ComboBoxElement> comboMajors;

        public ObservableCollection<ComboBoxElement> ComboMajors
        {
            get => comboMajors;
            set
            {
                comboMajors = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComboMajors"));
            }
        }

        private ObservableCollection<ComboBoxElement> comboClasses;

        public ObservableCollection<ComboBoxElement> ComboClasses
        {
            get => comboClasses;
            set
            {
                comboClasses = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ComboClasses"));
            }
        }
        private ObservableCollection<ListBoxElement> listBoxStudents;

        public ObservableCollection<ListBoxElement> ListBoxStudents
        {
            get => listBoxStudents;
            set
            {
                listBoxStudents = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListBoxStudents"));
            }
        }

        public string SelectedSchool { get; set; }
        public string SelectedMajor { get; set; }
        public string SelectedClass { get; set; }

        private string selectedStuID;
        public string SelectedStuID
        {
            get => selectedStuID;
            set
            {
                selectedStuID = value;
                SelectedStuData = db.FetchOneStudentData(selectedStuID);
            }
        }

        public StudentObject SelectedStuData { get; set; }


        public int DataGridSelectedIdx { get; set; }

        /// <summary>
        /// 获取学院/专业/班级列表
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        private List<string> GetSMCList(SMC smcTarget)
        {
            List<SMCObject> source = db.FetchSMC(smcTarget);
            List<string> ls = new List<string> { };
            source.ForEach(item => ls.Add(item.Name));
            return ls;
        }
        /// <summary>
        /// 获得班级-学生列表
        /// </summary>
        /// <returns></returns>
        private List<StudentObject> GetStudentsList()
        {
            List<StudentObject> ls = db.FetchStudents();
            return ls;
        }
        /// <summary>
        /// 载入学院列表
        /// </summary>
        public void LoadComboBoxSchool()
        {
            List<string> schoolsList = GetSMCList(SMC.Schools);
            ComboSchools = new ObservableCollection<ComboBoxElement> { };
            schoolsList.ForEach(item => { ComboSchools.Add(new ComboBoxElement { Text = item }); });
            ComboSchools.Add(new ComboBoxElement { Text = "--添加--" });

        }
        /// <summary>
        /// 载入专业列表
        /// </summary>
        public void LoadComboBoxMajor()
        {
            List<string> majorsList = GetSMCList(SMC.Majors);
            ComboMajors = new ObservableCollection<ComboBoxElement> { };
            majorsList.ForEach(item => { ComboMajors.Add(new ComboBoxElement { Text = item }); });
            ComboMajors.Add(new ComboBoxElement { Text = "--添加--" });
        }
        /// <summary>
        /// 载入班级列表
        /// </summary>
        public void LoadComboBoxClass()
        {
            List<string> classesList = GetSMCList(SMC.Classes);
            ComboClasses = new ObservableCollection<ComboBoxElement> { };
            classesList.ForEach(item => { ComboClasses.Add(new ComboBoxElement { Text = item }); });
            ComboClasses.Add(new ComboBoxElement { Text = "--添加--" });
        }
        /// <summary>
        /// 载入学生列表
        /// </summary>
        public void LoadListBoxStudent()
        {
            List<StudentObject> ls = GetStudentsList();
            ListBoxStudents = new ObservableCollection<ListBoxElement> { };
            ls.ForEach(item => { ListBoxStudents.Add(new ListBoxElement { Name = item.Name, ID = item.UID }); });
        }
        /// <summary>
        /// 载入学生所有成绩
        /// </summary>
        /// <param name="selectedStudent"></param>
        public void LoadDataGrid(string id)
        {
            SelectedStuID = id;
            List<GradeObject> ls = db.FetchGrades(SelectedStuID);
            DataGridSource = new ObservableCollection<GradeObject> { };
            ls.ForEach(item => DataGridSource.Add(item));
        }
        /// <summary>
        /// 应用表格内的修改到文件
        /// </summary>
        /// <param name="coursesItems"></param>
        public void UpdateData(ObservableCollection<GradeObject> coursesItems)
        {
            List<GradeObject> ls = new List<GradeObject> { };
            foreach (GradeObject item in coursesItems)
            {
                ls.Add(item);
            }
            db.UpdateGrades(ls, SelectedStuID);
        }

        public void RefreshSelectionBox()
        {
            //schoolRoot = js.SchoolLoad();
            if (SelectedSchool != null) { SelectedSchool = SelectedSchool; }
            if (SelectedMajor != null) { SelectedMajor = SelectedMajor; }
            if (SelectedClass != null) { SelectedClass = SelectedClass; }
        }

        public void AddComboBoxSchool()
        {
            MsgBoxAddItems msgBox = new MsgBoxAddItems
            {
                ApplyObj = "Schools"
            };
            msgBox.ShowDialog();
        }
        public void AddComboBoxMajor()
        {
            MsgBoxAddItems msgBox = new MsgBoxAddItems
            {
                ApplyObj = "Majors",
                SelectedSchool = SelectedSchool
            };
            msgBox.ShowDialog();
        }
        public void AddComboBoxClass()
        {
            MsgBoxAddItems msgBox = new MsgBoxAddItems
            {
                ApplyObj = "Classes",
                SelectedSchool = SelectedSchool,
                SelectedMajor = SelectedMajor
            };
            msgBox.ShowDialog();
        }

        public void AddListBoxStudent()
        {
            //if (SelectedClassObj == null) { SelectedClassObj = new ClassesItem { }; }
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

