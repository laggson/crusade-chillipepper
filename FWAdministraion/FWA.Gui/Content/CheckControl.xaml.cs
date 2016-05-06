﻿using System.Windows;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für DeviceCheck.xaml
    /// </summary>
    public partial class CheckControl : UserControl
    {
        MainWindow _parent;

        public CheckControl(MainWindow parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            _parent.CloseTab(this.GetHashCode());
        }

        private void Table_AutoGeneratingColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch(e.Column.Header.ToString())
            {
                case "DateChecked":
                    e.Cancel = true;
                    return;

                case "CheckType":
                    e.Column.IsReadOnly = false;
                    break;

                case "Lack":
                    e.Column.IsReadOnly = false;
                    break;

                case "Comment":
                    e.Column.IsReadOnly = false;
                    break;
                    
            }

            e.Column.Header = ((System.ComponentModel.PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }
    }
}