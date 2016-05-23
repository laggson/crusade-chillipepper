using FWA.Gui.Logic;
using FWA.Logic;
using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für FWControl.xaml
    /// </summary>
    public partial class OverviewControl : UserControl
    {
        /// <summary>
        /// Dunno what to write here. It's just an event, bro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DeviceEditedListener(object sender, DeviceEventArgs e);
        /// <summary>
        /// Will rise when a device was edited. Maybe used for storing the updated data, maybe removed later.
        /// </summary>
        public event DeviceEditedListener DeviceEdited;
        /// <summary>
        /// Will rise when a device in the table is double clicked. Needed to open the check tab
        /// </summary>
        public event DeviceEditedListener DeviceDoubleClicked;

        public DeviceCategory Category { get; set; }

        public OverviewControl(DeviceCategory category)
        {
            InitializeComponent();
            this.Name = category.DisplayName;
            Category = category;
        }

        /// <summary>
        /// Uses the transmitted DBAuthentication to receive all devices matching to this object's global category and writes them into 'Table'
        /// </summary>
        /// <param name="db"></param>
        public void LoadValuesFromDatabase(DBAuthentication db)
        {
            var list = db.GetDevicesByInvNumberType(Category.InvNumberLike);
            list = TrimList(list);
            Dispatcher.Invoke(() => Table.ItemsSource = list);
        }

        /// <summary>
        /// Clears the Table
        /// </summary>
        public void Clear()
        {
            Table.ItemsSource = null;
        }

        /// <summary>
        /// Returns a new List, which contains one and only one Item of each name
        /// </summary>
        /// <param name="source">The list of items for the specific location</param>
        /// <returns></returns>
        private List<Device> TrimList(IList<Device> source)
        {
            var list = new List<Device>();

            foreach (Device d in source)
            {
                //Check if any of the list items already has that name
                bool itemFound = list.Any(item => item.Name.Equals(d.Name));

                //If there's no item in the local list with the current name, insert it
                if (!itemFound)
                    list.Add(d);
            }

            return list;
        }

        /// <summary>
        /// Hides the ID column and overwrites the header of the other columns by the definded ComponentModel.DisplayName
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Table_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString().Equals("ID"))
                e.Cancel = true;
            else
                e.Column.Header = ((System.ComponentModel.PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }

        /// <summary>
        /// If the Table's Itemssource is not null, the DeviceDoubleClicked event is raised, containing the selected Device and the name of this tab.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Table_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (Table.ItemsSource != null)
            {
                DeviceDoubleClicked?.Invoke(this, new DeviceEventArgs
                {
                    Device = Table.SelectedItem as Device,
                    ControlName = Name
                });
            }
        }

        /// <summary>
        /// Is supposed to update the device object after it's data is changed in the Table. Not sure if I'm to leave this implemented...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Table_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            // TODO: Transmitted Data has old value.
            //       Must find a way to get the new one
            DeviceEdited?.Invoke(this, new DeviceEventArgs
            {
                Device = e.Row.Item as Device,
                ControlName = Name
            });
        }
    }

    public class DeviceEventArgs : EventArgs
    {
        public Device Device { get; set; }

        public string ControlName { get; set; }
    }
}
