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
using StudentManager.Access;
using StudentManager.Models;
using StudentManager.ViewModels;

namespace StudentManager.Views
{

    /// <summary>
    /// ManagePage.xaml 的交互逻辑
    /// </summary>
    public partial class ManagePage
    {
        private string selectedTerm = string.Empty;
        private string selectedClass = string.Empty;
        public int DataGridSelectedIdx { get; set; }

        private List<string> emptyList = new List<string> { };


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
        ManagePageViewModel managePageObj = new ManagePageViewModel();

        public ManagePage()
        {
            InitializeComponent();
            this.DataContext = managePageObj;

        }
        #region 表格选择
        private void TermsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedTerm = TermsListBox.SelectedItem.ToString();
            managePageObj.SelectionChanged(selectedTerm, selectedClass);
        }

        private void ClassesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedClass = ClassesListBox.SelectedItem.ToString();
            managePageObj.SelectionChanged(selectedTerm, selectedClass);
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
            managePageObj.UpdateDatabase(managePageObj.SelectedTable);
        }

        private void TableDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            btnSaveChange.IsEnabled = true;
            tbBottomInfo.Visibility = Visibility.Visible;
        }

        private void btnDelRow_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridSelectedIdx != -1 && DataGridSelectedIdx < managePageObj.DataGridSource.Count)
            {
                managePageObj.DataGridSource.RemoveAt(DataGridSelectedIdx);
            }
        }
    }
}
