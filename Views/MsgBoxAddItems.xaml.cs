using StudentManager.Models;
using StudentManager.ViewModels;
using System.Collections.Generic;
using System.Windows;

namespace StudentManager.Views
{
    /// <summary>
    /// MessageBoxAddItems.xaml 的交互逻辑
    /// </summary>
    public partial class MsgBoxAddItems : Window
    {
        public MsgBoxAddItems()
        {
            InitializeComponent();
            viewModelObj = new MsgBoxAddItemsViewModel();
            DataContext = viewModelObj;
        }

        private MsgBoxAddItemsViewModel viewModelObj;

        public string ApplyObj { get; set; }
        public string SelectedSchool { get; set; }
        public string SelectedMajor { get; set; }
        public string SelectedClass { get; set; }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Hidden;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            StuDataGrid.CanUserAddRows = false;
            viewModelObj.Apply(ApplyObj);
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void MenuItemDel_Click(object sender, RoutedEventArgs e)
        {
            int selected = viewModelObj.DataGridSelectedIdx;
            if (selected != -1 && selected < viewModelObj.DataGridSource.Count)
            {
                List<SMCObject> newList = viewModelObj.DataGridSource;
                newList.RemoveAt(selected);
                viewModelObj.DataGridSource = newList;
            }
        }
    }
}
