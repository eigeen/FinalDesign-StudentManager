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
        

        public void UpdateSchoolRoot(List<string> ls, string target)
        {
            var schoolRoot = this.SchoolLoad();
            if (target == "School")
            {
                ls.ForEach(e => schoolRoot.Schools.Add(
                    new SchoolsItem { Name = e, ID = "", Tag = "", Majors = new List<MajorsItem> { } }
                    ));
            }

            var str = JsonConvert.SerializeObject(schoolRoot);
            using (StreamWriter sw = new StreamWriter(schoolPath))
            {
                sw.Write(str);
            }
        }
    }

}
