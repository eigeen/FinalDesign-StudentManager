using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StudentManager.Model;
using StudentManager.ViewModel;

namespace StudentManager.View
{

    public enum Sex { 男, 女 }

    /// <summary>
    /// ManagePage.xaml 的交互逻辑
    /// </summary>
    public partial class ManagePage
    {
        ManagePageVM managePageVM = new ManagePageVM();

        public List<string> TermsList { get; set; } = new List<string> { };
        public List<string> ClassesList { get; set; } = new List<string> { };
        public string Term { get; set; } = string.Empty;
        public string Class { get; set; } = string.Empty;
        public string LinesInPage { get; set; }

        private List<string> emptyList = new List<string> { };

        InsertionView insertionView = new InsertionView();

        public ManagePage()
        {
            InitializeComponent();
            DataContext = this;

            //获取学期列表
            TermsList = managePageVM.GetTerms();

            //获取班级列表
            ClassesList = managePageVM.GetClasses();

        }

        private void TermsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Term = TermsListBox.SelectedItem.ToString();
            if (Class != string.Empty)
            {
                string tableName = managePageVM.FetchTableName(Term, Class);
                TableDataGrid.ItemsSource = managePageVM.FetchTable(tableName);
            }
            else { ClassesListBox.IsEnabled = true; }
        }

        private void ClassesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Class = ClassesListBox.SelectedItem.ToString();
            if (Term != string.Empty)
            {
                string tableName = managePageVM.FetchTableName(Term, Class);
                TableDataGrid.ItemsSource = managePageVM.FetchTable(tableName);
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            insertionView.Title = "插入行";
            insertionView.ShowDialog();
        }
    }
}
