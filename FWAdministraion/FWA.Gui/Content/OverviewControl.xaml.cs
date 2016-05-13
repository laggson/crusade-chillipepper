using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für FWControl.xaml
    /// </summary>
    public partial class OverviewControl : UserControl
    {
        MainWindow _main;

        public OverviewControl(MainWindow main, string name)
        {
            InitializeComponent();
            _main = main;
            this.Name = name;
        }

        private void Table_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = ((System.ComponentModel.PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }

        private void Table_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Table.ItemsSource != null)
            {
                _main.Test(Table.SelectedItem as Device, this.Name);

                //Zu Testzwecken:

                //Device d = Table.SelectedItem as Device;
                //Check  c = new Check(d)
                //{
                //    //Name and InvNumber set automatically in Constructor
                //    ID = d.ID,
                //    DateChecked = DateTime.Now,
                //    WhoChecked = _main.Control.ConnectedUser,
                //    CheckType = CheckType.OK
                //};
                //_main.Control.DBHandler.PushOrUpdateCheck(c);
            }
        }

        private void Table_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // TODO: Transmitted Data has old value.
            //       Must find a way to get the new one
            _main.Control.DBHandler.PushOrUpdateDevice(e.Row.Item as Device);
        }
    }
}
