using FWA.Gui.Logic;
using FWA.Logic;
using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

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
            if (this.Category.InvNumberLike.Equals(string.Empty))
                return source.ToList<Device>();

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
            else if (e.Column.Header.ToString().Equals("MonthsToCheck"))
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

        private void Table_OnRowLoading(object sender, DataGridRowEventArgs e)
        {

        }

        //#region Changing of rows - found in the web

        //private void AlterRow(DataGridRowEventArgs e)
        //{
        //    var cell = GetCell(Table, e.Row, 1);
        //    if (cell == null) return;

        //    var item = e.Row.Item as Device;
        //    if (item == null) return;

        //    //Hier den benötigten Monat auswählen. Gucke später, wie
        //    var value = item.MonthsToCheck[0];
        //        switch (value)
        //        {
        //            case CheckType.NotNeeded:
        //                //grau, disabled?
        //                cell.Background = Brushes.Gray;
        //                cell.Content = string.Empty;
        //                break;
        //            case CheckType.LacksFound:
        //                //rot
        //                cell.Background = Brushes.Red;
        //                cell.Content = string.Empty;
        //                break;
        //            case CheckType.Repaired:
        //                //blau
        //                cell.Background = Brushes.Blue;
        //                cell.Content = string.Empty;
        //                break;
        //            case CheckType.OK:
        //                //grün
        //                cell.Background = Brushes.Green;
        //                cell.Content = "Ok";
        //                break;
        //            default:
        //                //weiß
        //                cell.Background = Brushes.White;
        //                cell.Content = string.Empty;
        //                break;
        //        }
        //}

        //public static DataGridRow GetRow(DataGrid grid, int index)
        //{
        //    var row = grid.ItemContainerGenerator.ContainerFromIndex(index) as DataGridRow;

        //    if (row == null)
        //    {
        //        // may be virtualized, bring into view and try again
        //        grid.ScrollIntoView(grid.Items[index]);
        //        row = (DataGridRow)grid.ItemContainerGenerator.ContainerFromIndex(index);
        //    }
        //    return row;
        //}

        //public static T GetVisualChild<T>(Visual parent) where T : Visual
        //{
        //    T child = default(T);
        //    int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
        //    for (int i = 0; i < numVisuals; i++)
        //    {
        //        var v = (Visual)VisualTreeHelper.GetChild(parent, i);
        //        child = v as T ?? GetVisualChild<T>(v);
        //        if (child != null)
        //        {
        //            break;
        //        }
        //    }
        //    return child;
        //}

        //public static DataGridCell GetCell(DataGrid host, DataGridRow row, int columnIndex)
        //{
        //    if (row == null) return null;

        //    var presenter = GetVisualChild<DataGridCellsPresenter>(row);
        //    if (presenter == null) return null;

        //    // try to get the cell but it may possibly be virtualized
        //    var cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
        //    if (cell == null)
        //    {
        //        // now try to bring into view and retreive the cell
        //        host.ScrollIntoView(row, host.Columns[columnIndex]);
        //        cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
        //    }
        //    return cell;

        //}

        //#endregion
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
