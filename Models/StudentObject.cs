using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.Models
{
    public class StudentObject
    {
        //  create table Students
        //(
        //    uid text not null,
        //    name text,
        //    sex text,
        //    age         int,
        //    class text,
        //    grade_table text,
        //    desc        text
        //);

        public string UID { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public string Class { get; set; }
        public string GradeTable { get; set; }
        public string Desc { get; set; }
    }
}
