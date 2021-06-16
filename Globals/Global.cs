using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.Globals
{
    public static class Global
    {
        public static string DBPath { get; set; } = "data.db";
        public static string SelectedSchoolID { get; set; }
        public static string SelectedMajorID { get; set; }
        public static string SelectedClassID { get; set; }
        public static string SelectedStuID { get; set; }
    }
}
