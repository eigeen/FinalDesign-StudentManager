﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.Models
{
    public class Student
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public int Age { get; set; }
        public double Score { get; set; }
        public double GPA { get; set; }
    }
}