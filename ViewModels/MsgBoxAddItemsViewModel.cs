using System.Collections.Generic;
using StudentManager.Access;
using StudentManager.Models;

namespace StudentManager.ViewModels
{
    public class MsgBoxAddItemsViewModel
    {
        public MsgBoxAddItemsViewModel()
        {
            DataGridSource = new List<SMCObject> { };

        }

        private readonly SqliteAccess db = new SqliteAccess();

        public List<SMCObject> DataGridSource { get; set; }
        public int DataGridSelectedIdx { get; set; }

        //public List<StudentObject> GetApplyData()
        //{
        //    List<StudentObject> ls = new List<StudentObject> { };
        //    foreach (StudentObject item in DataGridSource)
        //    {
        //        ls.Add(item);
        //    }
        //    return ls;
        //}

        public void Apply(string tbName)
        {
            foreach (SMCObject item in DataGridSource)
            {
                db.AddSMC(tbName, item);
            }
        }
    }
}
