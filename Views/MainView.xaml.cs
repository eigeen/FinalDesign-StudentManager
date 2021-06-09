using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using StudentManager.Views;

namespace WpfProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //启动时跳到首页
            mainNavi.SelectedItem = mainNavi.MenuItems.OfType<ModernWpf.Controls.NavigationViewItem>().First();

        }

        private void NavigationView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                //contentFrame.Navigate(typeof(SampleSettingsPage));
            }
            else
            {
                var selectedItem = (ModernWpf.Controls.NavigationViewItem)args.SelectedItem;
                string selectedItemTag = (string)selectedItem.Tag;
                sender.Header = selectedItemTag;
                string pageName = $"StudentManager.Views.{selectedItemTag}";
                Type pageType = typeof(HomePage).Assembly.GetType(pageName);
                contentFrame.Navigate(pageType);
            }
        }

    }

    public class ControlPagesData : List<ControlInfoDataItem>
    {
        public ControlPagesData()
        {
            AddPage(typeof(HomePage), "Control Palette");
        }

        private void AddPage(Type pageType, string displayName = null)
        {
            Add(new ControlInfoDataItem(pageType, displayName));
        }
    }

    public class ControlInfoDataItem
    {
        public ControlInfoDataItem(Type pageType, string title = null)
        {
            PageType = pageType;
            Title = title ?? pageType.Name.Replace("Page", null);
        }

        public string Title { get; }

        public Type PageType { get; }

        public override string ToString()
        {
            return Title;
        }

    }
}