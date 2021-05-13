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
        /// 
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
        /// 土木一班
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
        /// 土木工程
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
        /// 土木工程学院
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
        /// 国际学院
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
