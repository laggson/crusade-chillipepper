using FWA.Logic.Storage;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    public partial class CheckControl : UserControl
    {
        /// <summary>
        /// Dieses UserControl beinhaltet die Überprüfungen aller Gegenstände mit dem gleichen Namen für einen bestimmten Monat
        /// </summary>
        public CheckControl()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Wird aufgerufen, sobald der Finish-Knopf aktiviert wird.
        /// </summary>
        public event ChecksFinishedListener ChecksFinished;
        /// <summary>
        /// Stellt die Methode dar, die neben der durchgeführten Prüfung über keine spezifischen Ereignisdaten verfügt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                case "ID":
                    e.Cancel = true;
                    return;

                case "MonthsToCheck":
                    e.Cancel = true;
                    return;

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
    
    /// <summary>
    /// Enthält die Ereignisdaten für den Abschluss einer Überprüfung, enthalten in einem Check Array
    /// </summary>
    public class ChecksFinishedEventArgs : EventArgs
    {
        /// <summary>
        /// Eine Sammlung der Prüfungen, die in die Datenbank gespeichert werden.
        /// </summary>
        public Check[] Checks { get; set; }
    }
}
