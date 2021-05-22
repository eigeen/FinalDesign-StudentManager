using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;

namespace StudentManager.ViewModels
{
    class MsgBoxAddStudentViewModel
    {
        public MsgBoxAddStudentViewModel()
        {
            ItemsControlSource = new ObservableCollection<AddStudentModel> { new AddStudentModel { } };
        }
        public ObservableCollection<AddStudentModel> ItemsControlSource { get; set; }

        public List<AddStudentModel> GetApplyData()
        {
            var ls = new List<AddStudentModel> { };
            foreach (var item in ItemsControlSource)
            {
                ls.Add(item);
            }
            return ls;
        }
    }
}
