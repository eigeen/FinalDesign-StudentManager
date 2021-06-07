using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StudentManager.Models;
using StudentManager.Views;

namespace StudentManager.Access
{
    public class JsonAccess
    {
        private string schoolPath;

        public string SchoolPath
        {
            get { return schoolPath; }
            set
            {
                schoolPath = value;
                if (!File.Exists(SchoolPath))
                {
                    var t = File.Create(SchoolPath);
                    t.Close();
                }

            }
        }


        private string gradePath;

        public string GradePath
        {
            get { return gradePath; }
            set
            {
                gradePath = value;
                if (!File.Exists(GradePath))
                {
                    var t = File.Create(GradePath);
                    t.Close();
                }
            }
        }

        public void InitDB()
        {
            if (!File.Exists(SchoolPath))
            {
                using StreamWriter sw = new StreamWriter("_SchoolData.json");
                sw.Write("{\"Version\":\"1.0.0\",\"DataVersion\":1,\"Schools\":[]}");
            }           
            if (!File.Exists(GradePath))
            {
                using StreamWriter sw = new StreamWriter("_GradeData.json");
                sw.Write("{\"Version\":\"1.0.0\",\"DataVersion\":1,\"Grades\":[]}");
            }
        }

        /// <summary>
        /// 解析School.Json
        /// </summary>
        /// <returns></returns>
        public SchoolRoot SchoolLoad()
        {
            string jsonStr;
            using (StreamReader sr = new StreamReader(SchoolPath, Encoding.UTF8))
            {
                jsonStr = sr.ReadToEnd();
            }
            SchoolRoot schoolRoot = JsonConvert.DeserializeObject<SchoolRoot>(jsonStr);
            return schoolRoot;
        }
        /// <summary>
        /// 解析Grade.Json
        /// </summary>
        /// <returns></returns>
        public GradeRoot GradeLoad()
        {
            string jsonStr;
            using (StreamReader sr = new StreamReader(GradePath, Encoding.UTF8))
            {
                jsonStr = sr.ReadToEnd();
            }
            GradeRoot gradeRoot = JsonConvert.DeserializeObject<GradeRoot>(jsonStr);
            return gradeRoot;
        }

        public void UpdateGrade(List<CoursesItem> coursesItems, string stuID)
        {
            var gradeRoot = GradeLoad();
            gradeRoot.Grades.Find(e => e.ID == stuID).Courses = coursesItems;
            var str = JsonConvert.SerializeObject(gradeRoot);
            using (StreamWriter sw = new StreamWriter(gradePath))
            {
                sw.Write(str);
            }
        }
        /// <summary>
        /// 更新SchoolRoot
        /// </summary>
        /// <param name="ls"></param>
        /// <param name="target"></param>
        /// <param name="school"></param>
        /// <param name="major"></param>
        /// <param name="className"></param>
        public void UpdateSchoolRoot(List<string> ls, string target, string school, string major, string className)
        {
            SchoolRoot schoolRoot = this.SchoolLoad();
            if (target == "School")
            {
                ls.ForEach(e => schoolRoot.Schools.Add(
                    new SchoolsItem { Name = e, ID = "", Tag = "", Majors = new List<MajorsItem> { } }
                    ));
            }
            else if (target == "Major")
            {
                if (schoolRoot.Schools.Find(e => e.Name == school).Majors == null)
                {
                    schoolRoot.Schools.Find(e => e.Name == school).Majors = new List<MajorsItem> { };
                }
                ls.ForEach(e => schoolRoot.Schools.Find(e => e.Name == school).Majors.Add(
                    new MajorsItem { Name = e, ID = "", Tag = "", Desc = "" }
                    ));
            }
            else if (target == "Class")
            {
                if (schoolRoot.Schools.Find(c => c.Name == school).Majors.Find(m => m.Name == major).Classes == null)
                {
                    schoolRoot.Schools.Find(c => c.Name == school).Majors.Find(m => m.Name == major).Classes = new List<ClassesItem> { };
                }
                ls.ForEach(e => schoolRoot.Schools.Find(c => c.Name == school).Majors.Find(m => m.Name == major).Classes.Add(
                    new ClassesItem { Name = e, Students = new List<StudentsItem> { }, StudentCount = 0 }
                    ));
            }
            else if (target == "Student")
            {
                ls.ForEach(e => schoolRoot.Schools.Find(e => e.Name == school).Majors.Find(e => e.Name == major).Classes.Find(e => e.Name == className).Students.Add(
                    new StudentsItem { Name = e, Age = 0, Desc = "", ID = "", Sex = -1 }
                    ));
            }

            string str = JsonConvert.SerializeObject(schoolRoot);
            using StreamWriter sw = new StreamWriter(schoolPath);
            sw.Write(str);
        }
        /// <summary>
        /// 更新GradeRoot
        /// </summary>
        /// <param name="ls"></param>
        public void UpdateGradeRoot(List<AddStudentModel> ls)
        {
            GradeRoot gradeRoot = GradeLoad();
            List<GradesItem> newls = new List<GradesItem> { };
            foreach (AddStudentModel item in ls)
            {
                newls.Add(new GradesItem { Name = item.StuName, ID = item.StuID, Courses = new List<CoursesItem> { } }); ;
            }
            newls.ForEach(item => gradeRoot.Grades.Add(item));

            string str = JsonConvert.SerializeObject(gradeRoot);
            using StreamWriter sw = new StreamWriter(gradePath);
            sw.Write(str);
        }
    }

}
