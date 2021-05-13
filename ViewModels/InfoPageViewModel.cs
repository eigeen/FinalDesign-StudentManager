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
            js = new JsonAccess();
            js.SchoolPath = "_SchoolData.json";
            js.GradePath = "_GradeData.json";

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
    }
}
