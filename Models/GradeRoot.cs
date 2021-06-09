using System.Collections.Generic;

namespace StudentManager.Models
{
    public class CoursesItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Credit { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double GPA { get; set; }

    }

    public class GradesItem
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
        public List<CoursesItem> Courses { get; set; }
    }

    public class GradeRoot
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
        public List<GradesItem> Grades { get; set; }
    }

}
