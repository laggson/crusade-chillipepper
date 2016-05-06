using FWA.Logic.Storage;
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
            //The ID Column is hidden bc it is unnecessary for use
            //if (e.Column.Header.ToString().Equals("ID"))
            //    e.Cancel = true;
            //else 
            //The Column header is set to the Display name instead of the internal name
            e.Column.Header = ((System.ComponentModel.PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }

        private void Table_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // TODO: Öffnet neues Fenster, wo die Items aufgelistet sind die mehrfach da sind
            // Bsp: Ein Verkehrsleitkegel wird angezeigt, bei doppeklick sieht man liste mit allen 10
            if (Table.ItemsSource != null)
            {
                _main.Test(Table.SelectedItem as Device, this.Name);

                //List<Check> items = new List<Check>();
                //Device d = Table.SelectedItem as Device;
                //items.Add(new Check(d));
                //MahApps.Metro.Controls.MetroWindow w = new MahApps.Metro.Controls.MetroWindow();
                //DataGrid dg = new DataGrid();
                //dg.ItemsSource = items;
                //w.Content = dg;
                //w.Show();
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
