using StudentManager.Access;
using StudentManager.Models;
using StudentManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// InfoPage.xaml 的交互逻辑
    /// </summary>
    public partial class InfoPage
    {
        public InfoPage()
        {
            InitializeComponent();
            infoPageObj = new InfoPageViewModel();
            this.DataContext = infoPageObj;
        }
        private InfoPageViewModel infoPageObj;

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            infoPageObj.LoadData();
            infoPageObj.ListBoxMajor = new ObservableCollection<ListBoxElement> { };
        }
    }
}
