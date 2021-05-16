using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading.Tasks;
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
        private ManagePageViewModel managePageObj;

        public ManagePage()
        {
            InitializeComponent();
            managePageObj = new ManagePageViewModel();
            this.DataContext = managePageObj;

        }


        #region ToggleButton
        private void tbtnInsert_Checked(object sender, RoutedEventArgs e)
        {
            tbtnInsert.Content = "编辑模式";
            IsEditMode = true;
            MenuItemDel.IsEnabled = true;

        }

        private void tbtnInsert_Unchecked(object sender, RoutedEventArgs e)
        {
            tbtnInsert.Content = "浏览模式";
            IsEditMode = false;
            MenuItemDel.IsEnabled = false;

        }
        #endregion

        private void btnSaveChange_Click(object sender, RoutedEventArgs e)
        {
            btnSaveChange.IsEnabled = false;
            tbBottomInfo.Visibility = Visibility.Hidden;
            managePageObj.UpdateData(managePageObj.DataGridSource);
        }

        private void TableDataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            btnSaveChange.IsEnabled = true;
            tbBottomInfo.Visibility = Visibility.Visible;
        }

        private void cbSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                if ((sender as ComboBox).SelectedValue.ToString() == "--添加--")
                {
                    (sender as ComboBox).SelectedIndex = -1;
                    managePageObj.AddComboBoxSchool();
                    managePageObj.RefreshComboBox();
                    managePageObj.LoadComboBoxSchool();
                    return;
                }
                cbMajor.SelectedIndex = -1;
                cbClass.SelectedIndex = -1;
                lbStudent.SelectedIndex = -1;
                managePageObj.SelectedSchool = (sender as ComboBox).SelectedValue.ToString();
                managePageObj.LoadComboBoxMajor();
            }
        }

        private void cbMajor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                if ((sender as ComboBox).SelectedValue.ToString() == "--添加--")
                {
                    (sender as ComboBox).SelectedIndex = -1;
                    managePageObj.AddComboBoxMajor();
                }
                cbClass.SelectedIndex = -1;
                lbStudent.SelectedIndex = -1;
                managePageObj.SelectedMajor = (sender as ComboBox).SelectedValue.ToString();
                managePageObj.LoadComboBoxClass();
            }
        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                if ((sender as ComboBox).SelectedValue.ToString() == "--添加--")
                {
                    (sender as ComboBox).SelectedIndex = -1;
                    managePageObj.AddComboBoxClass();
                }
                lbStudent.SelectedIndex = -1;
                managePageObj.SelectedClass = (sender as ComboBox).SelectedValue.ToString();
                managePageObj.LoadListBoxStudent();
            }
            else { lbStudent.ItemsSource = new ObservableCollection<ListBoxElement> { }; }
        }

        private void lbStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex != -1)
            {
                ListBoxElement item = (ListBoxElement)(sender as ListBox).SelectedItem;
                var selectedValue = item.Text;
                managePageObj.LoadDataGrid(selectedValue);
            }
        }

        private void MenuItemDel_Click(object sender, RoutedEventArgs e)
        {
            btnSaveChange.IsEnabled = true;
            int selected = managePageObj.DataGridSelectedIdx;
            if (selected != -1 && selected < managePageObj.DataGridSource.Count)
            {
                var newList = managePageObj.DataGridSource;
                newList.RemoveAt(selected);
                managePageObj.DataGridSource = newList;
            }
        }

        private void TableDataGrid_LostFocus(object sender, RoutedEventArgs e)
        {
            TableDataGrid.SelectedIndex = -1;
        }
    }
}
