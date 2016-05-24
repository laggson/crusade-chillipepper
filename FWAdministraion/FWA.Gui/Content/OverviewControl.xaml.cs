using FWA.Gui.Logic;
using FWA.Logic;
using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    public partial class OverviewControl : UserControl
    {
        /// <summary>
        ///  Stellt die Methode dar, die neben den Informationen über einen Gegenstand über keine spezifischen Ereignisdaten verfügt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void DeviceEditedListener(object sender, DeviceEventArgs e);
        /// <summary>
        /// Wird aufgerufen, sobald das Bearbeiten eines Gegenstandes beendet wurde. Könnte später entfernt werden
        /// </summary>
        public event DeviceEditedListener DeviceEdited;
        /// <summary>
        /// Wird aufgerufen, sobald ein Doppelklick auf einen Gegenstand der Tabelle ausgeführt wurde
        /// </summary>
        public event DeviceEditedListener DeviceDoubleClicked;
        
        /// <summary>
        /// Die Kategorie, mit dessen Bedingung die Gegenstände in diesen Tab einsortiert werden
        /// </summary>
        public DeviceCategory Category { get; set; }
        
        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="OverviewControl"/>-Klasse mit der angegebenen Kategorie
        /// </summary>
        /// <param name="category"></param>
        public OverviewControl(DeviceCategory category)
        {
            InitializeComponent();
            this.Name = category.DisplayName;
            Category = category;
        }

        /// <summary>
        /// Lädt die Gegenstände, auf die die Bedingung der Kategorie zutrifft und speichert einen jeden Namens in <see cref="Table"/>
        /// </summary>
        /// <param name="db"></param>
        public void LoadValuesFromDatabase(DBAuthentication db)
        {
            var list = db.GetDevicesByInvNumberType(Category.InvNumberLike);
            list = TrimList(list);
            Dispatcher.Invoke(() => Table.ItemsSource = list);
        }
        
        /// <summary>
        /// Leert <see cref="Table"/>
        /// </summary>
        public void Clear()
        {
            Table.ItemsSource = null;
        }
        
        /// <summary>
        /// Kürzt die eingegebene Liste von Gegenständen, sodass genau eins von jedem Namen übrig bleibt
        /// </summary>
        /// <param name="source">Die vollständige Liste von Gegenständen</param>
        /// <returns>Die gekürzte Liste mit genau einem Gegenstand jeden Namens</returns>
        private List<Device> TrimList(IList<Device> source)
        {
            var list = new List<Device>();

            foreach (Device d in source)
            {
                bool itemFound = list.Any(item => item.Name.Equals(d.Name));
                
                if (!itemFound)
                    list.Add(d);
            }

            return list;
        }
        
        private void Table_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString().Equals("ID"))
                e.Cancel = true;
            else
                e.Column.Header = ((System.ComponentModel.PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }
        
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

    /// <summary>
    /// Enthält die Daten für ein Ereignis in Verbindung mit einem Gegenstand und entählt sowohl diesen als auch den Namen des Tabs
    /// </summary>
    public class DeviceEventArgs : EventArgs
    {
        /// <summary>
        /// Der Gegenstand, für den das Ereignis aufgerufen wurde
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// Enthält den Namen des Tabs, in dem das Ereignis aufgerufen wurde
        /// </summary>
        public string ControlName { get; set; }
    }
}
