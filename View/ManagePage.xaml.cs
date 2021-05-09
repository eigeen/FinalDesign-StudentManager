using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public string SelectedTable { get; set; }
        public string LinesInPage { get; set; }
        public int DGSelectedIdx { get; set; }

        private List<string> emptyList = new List<string> { };

        private List<StudentModel> dgItemsSource;

        public List<StudentModel> DGItemsSource
        {
            get { return dgItemsSource; }
            set
            {
                dgItemsSource = value;
                TableDataGrid.ItemsSource = dgItemsSource;
            }
        }


        private bool isEditMode;

        public bool IsEditMode
        {
            get { return isEditMode; }
            set
            {
                isEditMode = value;
                if (isEditMode)
                {
                    TableDataGrid.CanUserAddRows = true;
                    TableDataGrid.IsReadOnly = false;
                }
                else
                {
                    TableDataGrid.CanUserAddRows = false;
                    TableDataGrid.IsReadOnly = true;
                }
            }
        }

        public ManagePage()
        {
            InitializeComponent();
            DataContext = this;

            //获取学期列表
            TermsList = managePageVM.GetTerms();

            //获取班级列表
            ClassesList = managePageVM.GetClasses();

        }
        #region 表格选择
        private void TermsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Term = TermsListBox.SelectedItem.ToString();
            if (Class != string.Empty)
            {
                SelectedTable = managePageVM.FetchTableName(Term, Class);
                DGItemsSource = managePageVM.FetchTable(SelectedTable);
            }
            else { ClassesListBox.IsEnabled = true; }
        }

        private void ClassesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Class = ClassesListBox.SelectedItem.ToString();
            if (Term != string.Empty)
            {
                SelectedTable = managePageVM.FetchTableName(Term, Class);
                DGItemsSource = managePageVM.FetchTable(SelectedTable);
            }
        }
        #endregion

        #region ToggleButton
        private void tbtnInsert_Checked(object sender, RoutedEventArgs e)
        {
            tbtnInsert.Content = "编辑模式";
            IsEditMode = true;
            btnDelRow.IsEnabled = true;
        }

        private void tbtnInsert_Unchecked(object sender, RoutedEventArgs e)
        {
            tbtnInsert.Content = "浏览模式";
            IsEditMode = false;
            btnDelRow.IsEnabled = false;
        }
        #endregion

        private void btnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            btnSaveChange.IsEnabled = false;
            tbBottomInfo.Visibility = Visibility.Hidden;
            managePageVM.UpdateDatabase(DGItemsSource, SelectedTable);
        }

        private void TableDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            btnSaveChange.IsEnabled = true;
            tbBottomInfo.Visibility = Visibility.Visible;
        }

        private void btnDelRow_Click(object sender, RoutedEventArgs e)
        {
            if (DGSelectedIdx != -1 && DGSelectedIdx < DGItemsSource.Count)
            {
                DGItemsSource.RemoveAt(DGSelectedIdx);
                TableDataGrid.ItemsSource = emptyList;
                TableDataGrid.ItemsSource = DGItemsSource;
            }
        }
    }
}
