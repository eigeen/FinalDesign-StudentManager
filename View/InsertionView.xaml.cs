using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudentManager.View
{
    /// <summary>
    /// InsertionView.xaml 的交互逻辑
    /// </summary>
    public partial class InsertionView : Window
    {
        public InsertionView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, tbID);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;  // cancels the window close    
            this.Hide();      // Programmatically hides the window
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            if (tbID.Text == "" || tbName.Text == "" || tbSex.Text == "" || tbAge.Text == "" || tbScore.Text == "" || tbGPA.Text == "")
            {
                MessageBox.Show("所有输入框均不能为空", "无法插入数据");
            }
            else
            {

            }
        }
    }
}
