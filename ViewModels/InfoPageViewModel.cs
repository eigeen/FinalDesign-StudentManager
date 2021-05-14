using StudentManager.Access;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace StudentManager.ViewModels
{
    public class InfoPageViewModel : INotifyPropertyChanged
    {
        public InfoPageViewModel()
        {
            js = new JsonAccess
            {
                SchoolPath = "_SchoolData.json",
                GradePath = "_GradeData.json"
            };

        }
        private JsonAccess js;
        private SchoolRoot schoolRoot;
        private GradeRoot gradeRoot;

        public event PropertyChangedEventHandler PropertyChanged;

        private ObservableCollection<ListBoxElement> listBoxSchool;

        public ObservableCollection<ListBoxElement> ListBoxSchool
        {
            get { return listBoxSchool; }
            set
            {
                listBoxSchool = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListBoxSchool"));
            }
        }

        private ObservableCollection<ListBoxElement> listBoxMajor;

        public ObservableCollection<ListBoxElement> ListBoxMajor
        {
            get { return listBoxMajor; }
            set
            {
                listBoxMajor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListBoxMajor"));
            }
        }

        private ObservableCollection<ListBoxElement> listBoxClass;

        public ObservableCollection<ListBoxElement> ListBoxClass
        {
            get { return listBoxClass; }
            set
            {
                listBoxClass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ListBoxClass"));
            }
        }

        public string SelectedSchool { get; set; }
        public string SelectedMajor { get; set; }

        public void LoadData()
        {
            schoolRoot = js.SchoolLoad();
            gradeRoot = js.GradeLoad();
            LoadListBoxSchool();
        }

        private void LoadListBoxSchool()
        {
            var schoolsList = GetSchoolsList();
            ListBoxSchool = new ObservableCollection<ListBoxElement> { };
            schoolsList.ForEach(item => { ListBoxSchool.Add(new ListBoxElement { Text = item }); });
        }

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

        public void LoadListBoxMajor()
        {
            var majorsList = GetMajorsList();
            ListBoxMajor = new ObservableCollection<ListBoxElement> { };
            majorsList.ForEach(item => { ListBoxMajor.Add(new ListBoxElement { Text = item }); });
        }

        private List<string> GetMajorsList()
        {
            var names = new List<string> { };
            schoolRoot.Schools.Find(e => e.Name == SelectedSchool).Majors.ForEach(e => names.Add(e.Name));
            return names;
        }

        public void LoadListBoxClass()
        {
            var classesList = GetClassesList();
            ListBoxClass = new ObservableCollection<ListBoxElement> { };
            classesList.ForEach(item => { ListBoxClass.Add(new ListBoxElement { Text = item }); });
        }

        private List<string> GetClassesList()
        {
            var names = new List<string> { };
            schoolRoot.Schools.Find(e => e.Name == SelectedSchool).Majors.Find(e => e.Name == SelectedMajor).Classes.ForEach(e => names.Add(e.Name));
            return names;
        }
    }
}
