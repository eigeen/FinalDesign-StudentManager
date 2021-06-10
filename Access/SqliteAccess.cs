using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using StudentManager.Models;

namespace StudentManager.Access
{
    public enum SMC
    {
        Schools,
        Majors,
        Classes
    }

    public class SqliteAccess
    {
        private static SqliteConnection conn;
        private static SqliteCommand cmd;
        private SqliteDataReader reader;
        public void Connect(string dbPath)
        {
            conn = new SqliteConnection($"Data Source={dbPath}");
            conn.Open();
            cmd = conn.CreateCommand();
            InitTable();
        }

        ~SqliteAccess()
        {
            conn.Dispose();
            cmd.Dispose();
        }

        private void InitTable()
        {
            if (!ExistTable("Schools"))
            {
                CreateTable("Schools");
            }
            if (!ExistTable("Majors"))
            {
                CreateTable("Majors");
            }
            if (!ExistTable("Classes"))
            {
                CreateTable("Classes");
            }
            if (!ExistTable("Students"))
            {
                CreateTableStudents();
            }
        }

        #region Utils
        private bool ExistTable(string tableName)
        {
            cmd.CommandText = @$"
SELECT name
FROM sqlite_master
WHERE type = 'table'
  AND name = '{tableName}'
";
            reader = cmd.ExecuteReader();
            bool isExist = reader.Read();
            reader.Close();
            return isExist;
        }

        private void RenameTable(string oldName, string newName)
        {
            cmd.CommandText = @$"
alter table '{oldName}'
rename to '{newName}';";
            cmd.ExecuteNonQuery();
        }

        private void DropTable(string tbName)
        {
            cmd.CommandText = @$"drop table '{tbName}';";
            cmd.ExecuteNonQuery();
        }
        #endregion

        #region Create Tables
        private void CreateTable(string tbName)
        {
            cmd.CommandText = @$"
CREATE TABLE '{tbName}'
(
    id        text not null,
    name      text,
    belong_to text,
    info      text
)";
            _ = cmd.ExecuteNonQuery();
        }

        private void CreateTableStudents()
        {
            cmd.CommandText = @"
CREATE TABLE 'Students'
(
    uid         text not null,
    name        text,
    sex         text,
    age         int,
    class       text,
    grade_table text,
    desc        text
)";
            _ = cmd.ExecuteNonQuery();
        }

        public void CreateTableGrades(string stuID)
        {
            cmd.CommandText = $@"
CREATE TABLE '{stuID}'
(
    sid     text not null,
    subject text,
    credit  real,
    score   real,
    gpa     real
)";
            _ = cmd.ExecuteNonQuery();
        }
        #endregion

        #region AddColumns
        public void AddSMC(string tbName, SMCObject obj)
        {
            cmd.CommandText = @$"
INSERT INTO '{tbName}'
VALUES ('{obj.ID}', '{obj.Name}', '{obj.BelongTo}', '{obj.Info}')";
            _ = cmd.ExecuteNonQuery();
        }

        public void AddStudent(StudentObject obj)
        {
            cmd.CommandText = @$"
INSERT INTO 'Students'
VALUES ('{obj.UID}', '{obj.Name}', '{obj.Sex}', 
        '{obj.Age}', '{obj.Class}', '{obj.GradeTable}', '{obj.Desc}')";
            _ = cmd.ExecuteNonQuery();
        }

        public void AddGrade(string stuID, GradeObject obj)
        {
            if (!ExistTable(stuID))
            {
                CreateTableGrades(stuID);
            }
            cmd.CommandText = @$"
INSERT INTO '{stuID}'
VALUES ('{obj.SID}', '{obj.Subject}', '{obj.Credit}', 
        '{obj.Score}', '{obj.GPA}')";
            _ = cmd.ExecuteNonQuery();
        }
        #endregion

        private string ParseSMCEnum(SMC smcTarget)
        {
            string tbName = smcTarget switch
            {
                SMC.Schools => "Schools",
                SMC.Majors => "Majors",
                SMC.Classes => "Classes",
                _ => "Schools",
            };
            return tbName;
        }

        public List<SMCObject> FetchSMC(SMC smcTarget)
        {
            string tbName = ParseSMCEnum(smcTarget);
            cmd.CommandText = $"SELECT * FROM '{tbName}'";
            reader = cmd.ExecuteReader();

            List<SMCObject> ls = new List<SMCObject> { };
            while (reader.Read())
            {
                ls.Add(new SMCObject
                {
                    ID = reader.GetString(0),
                    Name = reader.GetString(1),
                    BelongTo = reader.GetString(2),
                    Info = reader.GetString(3)
                });
            }
            reader.Close();
            return ls;
        }

        public SMCObject FetchOneSMCbyID(SMC smcTarget, string id)
        {
            string tbName = ParseSMCEnum(smcTarget);
            cmd.CommandText = $"SELECT * FROM '{tbName}' WHERE id={id}";
            reader = cmd.ExecuteReader();
            SMCObject obj = new SMCObject { };
            if (reader.Read())
            {
                obj = new SMCObject
                {
                    ID = reader.GetString(0),
                    Name = reader.GetString(1),
                    BelongTo = reader.GetString(2),
                    Info = reader.GetString(3)
                };
            }
            reader.Close();
            return obj;
        }

        public SMCObject FetchOneSMCbyName(SMC smcTarget, string name)
        {
            string tbName = ParseSMCEnum(smcTarget);
            cmd.CommandText = $"SELECT * FROM '{tbName}' WHERE name='{name}';";
            reader = cmd.ExecuteReader();
            SMCObject obj = new SMCObject { };
            if (reader.Read())
            {
                obj = new SMCObject
                {
                    ID = reader.GetString(0),
                    Name = reader.GetString(1),
                    BelongTo = reader.GetString(2),
                    Info = reader.GetString(3)
                };
            }
            reader.Close();
            return obj;
        }

        public List<StudentObject> FetchStudents()
        {
            cmd.CommandText = $"SELECT * FROM 'Students'";
            reader = cmd.ExecuteReader();

            List<StudentObject> ls = new List<StudentObject> { };
            while (reader.Read())
            {
                ls.Add(new StudentObject
                {
                    UID = reader.GetString(0),
                    Name = reader.GetString(1),
                    Sex = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    Class = reader.GetString(4),
                    GradeTable = reader.GetString(5),
                    Desc = reader.GetString(6),
                });
            }
            reader.Close();
            return ls;
        }

        public List<GradeObject> FetchGrades(string stuID)
        {
            if (!ExistTable(stuID))
            {
                CreateTableGrades(stuID);
            }
            cmd.CommandText = $"SELECT * FROM '{stuID}'";
            reader = cmd.ExecuteReader();

            List<GradeObject> ls = new List<GradeObject> { };
            while (reader.Read())
            {
                ls.Add(new GradeObject
                {
                    SID = reader.GetString(0),
                    Subject = reader.GetString(1),
                    Credit = reader.GetDouble(2),
                    Score = reader.GetDouble(3),
                    GPA = reader.GetDouble(4)
                });
            }
            reader.Close();
            return ls;
        }

        public StudentObject FetchOneStudentData(string stuID)
        {
            StudentObject obj = new StudentObject { };
            cmd.CommandText = $@"SELECT * FROM 'Students' WHERE uid='{stuID}';";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                obj = new StudentObject
                {
                    UID = reader.GetString(0),
                    Name = reader.GetString(1),
                    Sex = reader.GetString(2),
                    Age = reader.GetInt32(3),
                    Class = reader.GetString(4),
                    GradeTable = reader.GetString(5),
                    Desc = reader.GetString(6),
                };
            }
            reader.Close();
            return obj;
        }

        public List<GradeObject> FetchOneStudentGrades(string stuID)
        {
            cmd.CommandText = $"SELECT * FROM '{stuID}'";
            reader = cmd.ExecuteReader();

            List<GradeObject> ls = new List<GradeObject> { };
            while (reader.Read())
            {
                ls.Add(new GradeObject
                {
                    SID = reader.GetString(0),
                    Subject = reader.GetString(1),
                    Credit = reader.GetDouble(2),
                    Score = reader.GetDouble(3),
                    GPA = reader.GetDouble(4),
                });
            }
            reader.Close();
            return ls;
        }


        public void UpdateGrades(List<GradeObject> ls, string stuID)
        {
            string tmpName = stuID + "_tmp";
            if (ExistTable(tmpName))
            {
                DropTable(tmpName);
            }
            CreateTableGrades(tmpName);
            foreach (GradeObject item in ls)
            {
                AddGrade(tmpName, item);
            }
            DropTable(stuID);
            RenameTable(tmpName, stuID);
        }
    }
}
