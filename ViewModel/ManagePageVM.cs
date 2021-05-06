using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.Sqlite;
using StudentManager.Model;

namespace StudentManager.ViewModel
{
    class ManagePageVM
    {
        private SqliteConnection conn;
        private SqliteCommand cmd;
        private SqliteDataReader reader;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ManagePageVM()
        {
            conn = new SqliteConnection("Data Source = Data.db");
            conn.Open();
            cmd = conn.CreateCommand();
        }

        /// <summary>
        /// 若数据库为空，则新建Info表
        /// </summary>
        private void InitDB()
        {
            cmd.CommandText = "SELECT * FROM sqlite_master WHERE table='Info'";
            reader = cmd.ExecuteReader();
            if (!reader.Read())
            {
                cmd.CommandText = @"CREATE TABLE 'Info' (
                                        Term TEXT NOT NULL,
	                                    Class TEXT NOT NULL,
	                                    TableName TEXT NOT NULL,
                                        Subjects TEXT)";
                cmd.ExecuteNonQuery();
            }
            reader.Close();
        }

        #region ListBox列表获取
        /// <summary>
        /// 获得学期列表
        /// </summary>
        /// <returns>学期名</returns>
        public List<string> GetTerms()
        {
            cmd.CommandText = "SELECT DISTINCT Term FROM Info;";
            reader = cmd.ExecuteReader();

            List<string> terms = new List<string> { };
            int termIdx = reader.GetOrdinal("Term");
            while (reader.Read())
            {
                string termName = reader.GetString(termIdx);
                terms.Add(termName);
            }
            reader.Close();
            //terms.Sort();
            //terms.Reverse();
            return terms;
        }
        /// <summary>
        /// 获得班级列表
        /// </summary>
        /// <returns>班级名</returns>
        public List<string> GetClasses()
        {
            cmd.CommandText = "SELECT DISTINCT Class FROM Info;";
            reader = cmd.ExecuteReader();

            List<string> classes = new List<string> { };
            int classIdx = reader.GetOrdinal("Class");
            while (reader.Read())
            {
                string className = reader.GetString(classIdx);
                classes.Add(className);
            }
            reader.Close();
            //classes.Sort();
            //classes.Reverse();
            return classes;
        }
        #endregion

        /// <summary>
        /// 创建Term_Class表
        /// </summary>
        /// <param name="tableName"></param>
        public void CreateTable(string tableName)
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
        /// 获取表的内容
        /// </summary>
        /// <param name="tableName">源表名</param>
        /// <returns>DataGrid所需的数据源</returns>
        public List<StudentModel> FetchTable(string tableName)
        {
            if (!ExistTable(tableName))
            {
                CreateTable(tableName);
                return new List<StudentModel> { };
            }

            cmd.CommandText = $"SELECT * FROM {tableName}";
            reader = cmd.ExecuteReader();

            var columns = new List<StudentModel> { };
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
                columns.Add(new StudentModel()
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
            return columns;
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
        public string FetchTableName(string termName, string className)
        {
            cmd.CommandText = $"SELECT * FROM Info WHERE Term='{termName}' AND Class='{className}';";
            reader = cmd.ExecuteReader();
            int idx = reader.GetOrdinal("TableName");
            reader.Read();
            string tableName = reader.GetString(idx);
            reader.Close();
            return tableName;
        }
    }
}
