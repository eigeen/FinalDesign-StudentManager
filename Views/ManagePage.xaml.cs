using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ModernWpf.Controls;
using StudentManager.Models;
using StudentManager.ViewModels;

namespace StudentManager.Views
{

    /// <summary>
    /// ManagePage.xaml 的交互逻辑
    /// </summary>
    public partial class ManagePage
    {
        private static int isFirstAccess = 0;
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
        private ManagePageViewModel managePageObj = new ManagePageViewModel();

        public ManagePage()
        {
            InitializeComponent();

            DataContext = managePageObj;

        }

        private void SwitchInsertMode_Toggled(object sender, RoutedEventArgs e)
        {
            if ((sender as ToggleSwitch).IsOn == true)
            {
                IsEditMode = true;
                MenuItemDel.IsEnabled = true;
            }
            else
            {
                IsEditMode = false;
                MenuItemDel.IsEnabled = false;
            }
        }

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
                if ((sender as ComboBox).SelectedValue.ToString() == "ADD")
                {
                    (sender as ComboBox).SelectedIndex = -1;
                    managePageObj.AddComboBoxSchool();
                    managePageObj.RefreshSelectionBox();
                    managePageObj.LoadComboBoxSchool();
                    return;
                }
                cbMajor.SelectedIndex = -1;
                cbClass.SelectedIndex = -1;
                lbStudent.SelectedIndex = -1;
                lbStudent.ItemsSource = new Dictionary<string, string> { };
                managePageObj.SelectedSchoolID = (sender as ComboBox).SelectedValue.ToString();
                managePageObj.LoadComboBoxMajor();
            }
        }

        private void cbMajor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                if ((sender as ComboBox).SelectedValue.ToString() == "ADD")
                {
                    (sender as ComboBox).SelectedIndex = -1;
                    managePageObj.AddComboBoxMajor();
                    managePageObj.RefreshSelectionBox();
                    managePageObj.LoadComboBoxMajor();
                    return;
                }
                cbClass.SelectedIndex = -1;
                lbStudent.SelectedIndex = -1;
                lbStudent.ItemsSource = new Dictionary<string, string> { };
                managePageObj.SelectedMajorID = (sender as ComboBox).SelectedValue.ToString();
                managePageObj.LoadComboBoxClass();
            }
        }

        private void cbClass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex != -1)
            {
                if ((sender as ComboBox).SelectedValue.ToString() == "ADD")
                {
                    (sender as ComboBox).SelectedIndex = -1;
                    managePageObj.AddComboBoxClass();
                    managePageObj.RefreshSelectionBox();
                    managePageObj.LoadComboBoxClass();
                    return;
                }
                lbStudent.ItemsSource = new Dictionary<string, string> { };
                managePageObj.SelectedClassID = (sender as ComboBox).SelectedValue.ToString();
                managePageObj.LoadListBoxStudent();
            }
            else { lbStudent.ItemsSource = new ObservableCollection<ListBoxElement> { }; }
        }

        private void lbStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ListBox).SelectedIndex != -1)
            {
                ListBoxElement item = (ListBoxElement)(sender as ListBox).SelectedItem;
                string selectedID = item.ID;
                managePageObj.LoadDataGrid(selectedID);
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

        private void BtnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            managePageObj.AddListBoxStudent();
            managePageObj.RefreshSelectionBox();
            managePageObj.LoadListBoxStudent();
        }

        private void cbSchool_Selected(object sender, RoutedEventArgs e)
        {
            if (isFirstAccess == 0)
            {
                managePageObj.Init();
                isFirstAccess = 1;
            }
        }
    }
}
