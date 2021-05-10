using System;
using System.Collections.Generic;
using System.Text;
using StudentManager.Access;
using StudentManager.Common;
using StudentManager.Models;

namespace StudentManager.ViewModels
{
    class ManagePageViewModel : NotificationObject
    {
        private DataAccess access = new DataAccess();
        private TableSelection tableSelectionObj = new TableSelection();

        /// <summary>
        /// 构造函数
        /// </summary>
        public ManagePageViewModel()
        {
            //获取学期列表
            TermsList = access.FetchInfoColumn("Term");
            //获取班级列表
            ClassesList = access.FetchInfoColumn("Class");

        }

        public List<string> TermsList
        {
            get { return tableSelectionObj.TermsList; }
            set
            {
                tableSelectionObj.TermsList = value;
                this.RaisePropertyChanged("TermsList");
            }
        }
        public List<string> ClassesList
        {
            get { return tableSelectionObj.ClassesList; }
            set
            {
                tableSelectionObj.ClassesList = value;
                this.RaisePropertyChanged("ClassesList");
            }
        }

        private List<Student> dataGridSource;

        public List<Student> DataGridSource
        {
            get { return dataGridSource; }
            set
            {
                dataGridSource = value;
                this.RaisePropertyChanged("DataGridSource");
            }
        }

        public string SelectedTable { get; set; }

        /// <summary>
        /// 学期或班级选项发生改变时更新表
        /// </summary>
        /// <param name="termName">学期名</param>
        /// <param name="className">班级名</param>
        /// <returns></returns>
        public void SelectionChanged(string termName, string className)
        {
            if (className != string.Empty && termName != string.Empty)
            {
                var ls = new List<Student> { };
                SelectedTable = access.GetTableName(termName, className);
                DataGridSource = access.FetchTable(SelectedTable);
            }
        }

        /// <summary>
        /// 应用修改到数据库
        /// </summary>
        /// <param name="tableName">表名</param>
        public void UpdateDatabase(string tableName)
        {
            access.UpdateDatabase(DataGridSource, tableName);
        }
    }
}

