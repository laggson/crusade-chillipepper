using FWA.Logic.Storage;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für DeviceCheck.xaml
    /// </summary>
    public partial class CheckControl : UserControl
    {
        public CheckControl()
        {
            InitializeComponent();
        }

        public event ChecksFinishedListener ChecksFinished;
        public delegate void ChecksFinishedListener(object sender, ChecksFinishedEventArgs e);

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            ChecksFinished?.Invoke(this, new ChecksFinishedEventArgs
                                         {
                                             Checks = Table.Items.OfType<Check>().ToArray()
                                         });
        }

        private void Table_AutoGeneratingColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
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

                default:
                    e.Column.IsReadOnly = true;
                    break;

            }

            e.Column.Header = ((System.ComponentModel.PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }
    }

    public class ChecksFinishedEventArgs : EventArgs
    {
        public Check[] Checks { get; set; }
    }
}
