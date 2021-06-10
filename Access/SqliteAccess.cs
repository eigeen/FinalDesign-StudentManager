using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using StudentManager.Models;

namespace StudentManager.Access
{

    public class SqliteAccess
    {
        private SqliteConnection conn;
        private SqliteCommand cmd;
        private SqliteDataReader reader;
        public void Connect(string dbPath)
        {
            conn = new SqliteConnection($"Data Source={dbPath}");
            cmd = conn.CreateCommand();
            InitTable();
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

        private bool ExistTable(string tableName)
        {
            cmd.CommandText = @$"
SELECT name
FROM sqlite_master
WHERE type = 'table'
  AND name = '{tableName}'
";
            reader = cmd.ExecuteReader();
            return reader.Read();
        }

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

        public void AddSMC(int tbName, SMCObject obj)
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
        '{obj.Age}', '{obj.Class}, '{obj.GradeTable}', '{obj.Desc}')";
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
        '{obj.Score}', '{obj.GPA})";
            _ = cmd.ExecuteNonQuery();
        }
    }
}
