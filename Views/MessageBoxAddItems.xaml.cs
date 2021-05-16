using StudentManager.Access;
using StudentManager.ViewModels;
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

namespace StudentManager.Views
{
    /// <summary>
    /// MessageBoxAddItems.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxAddItems : Window
    {
        public MessageBoxAddItems()
        {
            InitializeComponent();
            viewModelObj = new MessageBoxAddItemsViewModel();
            this.DataContext = viewModelObj;
            

            js = new JsonAccess
            {
                SchoolPath = "_SchoolData.json"
            };
        }

        private MessageBoxAddItemsViewModel viewModelObj;
        private JsonAccess js;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        private void BtnApply_Click(object sender, RoutedEventArgs e)
        {
            var ls = viewModelObj.GetApplyData();
            js.UpdateSchoolRoot(ls, "School");
            Close();
        }

        private void BtnCancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnDelRow_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
