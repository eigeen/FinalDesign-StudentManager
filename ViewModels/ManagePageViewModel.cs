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
            if (isFirstAccess)
            {
                Init();
                isFirstAccess = false;
            }
        }

        private static bool isFirstAccess = true;

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

        public string SelectedSchoolID { get; set; }
        public string SelectedMajorID { get; set; }
        public string SelectedClassID { get; set; }

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

        public void Init()
        {
            db.Connect();
            LoadComboBoxSchool();
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
            List<SMCObject> source = db.FetchSMC(SMC.Schools);
            ComboSchools = new ObservableCollection<ComboBoxElement> { };
            source.ForEach(item => { ComboSchools.Add(new ComboBoxElement { Name = item.Name, ID = item.ID }); });
            ComboSchools.Add(new ComboBoxElement { Name = "--添加--", ID = "ADD" });

        }
        /// <summary>
        /// 载入专业列表
        /// </summary>
        public void LoadComboBoxMajor()
        {
            List<SMCObject> source = db.FetchSMC(SMC.Majors);
            ComboMajors = new ObservableCollection<ComboBoxElement> { };
            source.ForEach(item => { ComboMajors.Add(new ComboBoxElement { Name = item.Name, ID = item.ID }); });
            ComboMajors.Add(new ComboBoxElement { Name = "--添加--" });
        }
        /// <summary>
        /// 载入班级列表
        /// </summary>
        public void LoadComboBoxClass()
        {
            List<SMCObject> source = db.FetchSMC(SMC.Classes);
            ComboClasses = new ObservableCollection<ComboBoxElement> { };
            source.ForEach(item => { ComboClasses.Add(new ComboBoxElement { Name = item.Name, ID = item.ID }); });
            ComboClasses.Add(new ComboBoxElement { Name = "--添加--" });
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
            if (SelectedSchoolID != null) { SelectedSchoolID = SelectedSchoolID; }
            if (SelectedMajorID != null) { SelectedMajorID = SelectedMajorID; }
            if (SelectedClassID != null) { SelectedClassID = SelectedClassID; }
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
                SelectedSchool = SelectedSchoolID
            };
            msgBox.ShowDialog();
        }
        public void AddComboBoxClass()
        {
            MsgBoxAddItems msgBox = new MsgBoxAddItems
            {
                ApplyObj = "Classes",
                SelectedSchool = SelectedSchoolID,
                SelectedMajor = SelectedMajorID
            };
            msgBox.ShowDialog();
        }

        public void AddListBoxStudent()
        {
            //if (SelectedClassObj == null) { SelectedClassObj = new ClassesItem { }; }
            MsgBoxAddStudent msgBox = new MsgBoxAddStudent
            {
                SelectedSchool = SelectedSchoolID,
                SelectedMajor = SelectedMajorID,
                SelectedClass = SelectedClassID
            };
            msgBox.ShowDialog();
        }

    }
}

