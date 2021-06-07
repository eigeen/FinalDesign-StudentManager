using StudentManager.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StudentManager.ViewModels
{
    public class MsgBoxAddStudentViewModel
    {
        public MsgBoxAddStudentViewModel()
        {
            ItemsControlSource = new ObservableCollection<AddStudentModel> { new AddStudentModel { StuID = "000", StuName = "姓名", StuSex = "男", StuAge = 19, StuDesc = "描述" } };
        }
        public ObservableCollection<AddStudentModel> ItemsControlSource { get; set; }

        public List<AddStudentModel> GetApplyData()
        {
            List<AddStudentModel> ls = new List<AddStudentModel> { };
            foreach (AddStudentModel item in ItemsControlSource)
            {
                ls.Add(item);
            }
            return ls;
        }
    }
}
