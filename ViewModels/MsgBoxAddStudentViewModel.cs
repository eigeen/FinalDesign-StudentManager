using StudentManager.Access;
using StudentManager.Models;
using System.Collections.Generic;

namespace StudentManager.ViewModels
{
    public class MsgBoxAddStudentViewModel
    {
        public MsgBoxAddStudentViewModel()
        {
            DataGridSource = new List<StudentObject> { };
            db = new SqliteAccess();
        }
        public List<StudentObject> DataGridSource { get; set; }
        private readonly SqliteAccess db;

        public void Apply(string className)
        {
            foreach (StudentObject item in DataGridSource)
            {
                item.Class = db.FetchOneSMCbyName(SMC.Classes, className).ID;
                db.AddStudent(item);
            }
        }
    }
}
