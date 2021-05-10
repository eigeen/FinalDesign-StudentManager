using Microsoft.Data.Sqlite;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManager.Access
{

    class DataAccess
    {
        private SqliteConnection conn;
        private SqliteCommand cmd;
        private SqliteDataReader reader;

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataAccess()
        {
            conn = new SqliteConnection("Data Source = Data.db");
            conn.Open();
            cmd = conn.CreateCommand();
            InitDB();
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~DataAccess()
        {
            conn.Close();
        }

        /// <summary>
        /// 若首次创建数据库，则新建Info表
        /// </summary>
        private void InitDB()
        {
            if (!ExistTable("Info"))
            {
                cmd.CommandText = "SELECT * FROM sqlite_master WHERE 'table'='Info'";
                reader = cmd.ExecuteReader();
                bool existInfoTable = !reader.Read();
                reader.Close();
                if (existInfoTable)
                {
                    reader.Close();
                    cmd.CommandText = @"CREATE TABLE 'Info' (
                                        Term TEXT NOT NULL,
	                                    Class TEXT NOT NULL,
	                                    TableName TEXT NOT NULL,
                                        Subjects TEXT)";
                    cmd.ExecuteNonQuery();

                }
            }
        }

        /// <summary>
        /// 获取Info表内指定列内容（去重）
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="isDistinct">是否去重</param>
        /// <returns></returns>
        public List<string> FetchInfoColumn(string columnName, bool isDistinct = true)
        {
            if (isDistinct)
            {
                cmd.CommandText = $"SELECT DISTINCT {columnName} FROM Info";
            }
            else
            {
                cmd.CommandText = $"SELECT {columnName} FROM Info";
            }
            reader = cmd.ExecuteReader();

            List<string> ls = new List<string> { };
            int cellIdx = reader.GetOrdinal(columnName);
            while (reader.Read())
            {
                string cell = reader.GetString(cellIdx);
                ls.Add(cell);
            }
            reader.Close();
            return ls;
        }

        /// <summary>
        /// 创建Term_Class表
        /// </summary>
        /// <param name="tableName">表名</param>
        public void CreateModelTable(string tableName)
        {
            cmd.CommandText = @$"CREATE TABLE {tableName} (
                                    ID TEXT NOT NULL, 
                                    Name TEXT NOT NULL, 
                                    Sex TEXT NOT NULL, 
                                    Age INTEGER NOT NULL, 
                                    Score REAL, 
                                    GPA REAL)";
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// 获取Model表的内容
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public List<Student> FetchTable(string tableName)
        {
            if (!ExistTable(tableName))
            {
                CreateModelTable(tableName);
                return new List<Student> { };
            }

            cmd.CommandText = $"SELECT * FROM {tableName}";
            reader = cmd.ExecuteReader();

            var rows = new List<Student> { };
            int IDIdx = reader.GetOrdinal("ID");
            int NameIdx = reader.GetOrdinal("Name");
            int SexIdx = reader.GetOrdinal("Sex");
            int AgeIdx = reader.GetOrdinal("Age");
            int ScoreIdx = reader.GetOrdinal("Score");
            int GPAIdx = reader.GetOrdinal("GPA");
            while (reader.Read())
            {
                string id = reader.GetString(IDIdx);
                string name = reader.GetString(NameIdx);
                string sex = reader.GetString(SexIdx);
                int age = reader.GetInt32(AgeIdx);
                double score = reader.GetDouble(ScoreIdx);
                double GPA = reader.GetDouble(GPAIdx);
                rows.Add(new Student()
                {
                    Id = id,
                    Name = name,
                    Sex = sex,
                    Age = age,
                    Score = score,
                    GPA = GPA
                });
            }
            reader.Close();
            return rows;
        }

        /// <summary>
        /// 是否存在某个表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>true / false</returns>
        private bool ExistTable(string tableName)
        {
            cmd.CommandText = $"SELECT name FROM sqlite_master WHERE name='{tableName}'";
            reader = cmd.ExecuteReader();
            bool existTable = reader.Read() ? true : false;
            reader.Close();
            return existTable;
        }

        /// <summary>
        /// 获取 学期+班级 对应的表名
        /// </summary>
        /// <param name="termName">学期名</param>
        /// <param name="className">班级名</param>
        /// <returns>表名</returns>
        public string GetTableName(string termName, string className)
        {
            cmd.CommandText = $"SELECT * FROM Info WHERE Term='{termName}' AND Class='{className}';";
            reader = cmd.ExecuteReader();
            int idx = reader.GetOrdinal("TableName");
            reader.Read();
            string tableName = reader.GetString(idx);
            reader.Close();
            return tableName;
        }

        /// <summary>
        /// 更新数据库
        /// </summary>
        /// <param name="data">源数据</param>
        /// <param name="tableName">Model表名</param>
        public void UpdateDatabase(List<Student> data, string tableName)
        {
            //删除表
            cmd.CommandText = $"DROP TABLE {tableName}";
            cmd.ExecuteNonQuery();

            //新建表
            CreateModelTable(tableName);

            foreach (Student row in data)
            {
                //插入新数据
                cmd.CommandText = @$"INSERT INTO {tableName} VALUES (
                    '{row.Id}','{row.Name}','{row.Sex}','{row.Age}','{row.Score}','{row.GPA}');";
                cmd.ExecuteNonQuery();
            }
        }

    }
}
