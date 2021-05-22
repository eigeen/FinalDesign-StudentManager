using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.Models
{
    public class StudentsItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Desc { get; set; }
    }

    public class ClassesItem
    {
        /// <summary>
        /// 班级名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int StudentCount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<StudentsItem> Students { get; set; }
    }

    public class MajorsItem
    {
        /// <summary>
        /// 专业名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Desc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ClassesItem> Classes { get; set; }
    }

    public class SchoolsItem
    {
        /// <summary>
        /// 学院名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MajorsItem> Majors { get; set; }
    }

    public class SchoolRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int DataVersion { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<SchoolsItem> Schools { get; set; }
    }

}
