using FWA.Logic.Storage;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// A UserControl for checking a group of devices and is displayed as a tab for the Dragablz Tab menu
    /// </summary>
    public partial class CheckControl : UserControl
    {
        /// <summary>
        /// Initializes the object (o_O)
        /// </summary>
        public CheckControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Will rise when the finish button is hit.
        /// </summary>
        public event ChecksFinishedListener ChecksFinished;
        /// <summary>
        /// Will rise when the finish button is hit.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void ChecksFinishedListener(object sender, ChecksFinishedEventArgs e);

        /// <summary>
        /// Raises the ChecksFinished event after the finish button is pressed.
        /// ChecksFinished contains an array of all the added Check objects
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            ChecksFinished?.Invoke(this, new ChecksFinishedEventArgs
                                         {
                                             Checks = Table.Items.OfType<Check>().ToArray()
                                         });
        }

        /// <summary>
        /// This is internally called on every column that is generated.
        /// Makes the ID and DateChecked column not visible, sets the ReadOnly values for the others 
        /// and overwrites the column headers for the defined ComponentModel.DisplayName attributes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Table_AutoGeneratingColumns(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString())
            {
                case "ID":
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
    /// Event args that contain the array of the freshly created checks, which are supposed to be pushed to the DB
    /// </summary>
    public class ChecksFinishedEventArgs : EventArgs
    {
        /// <summary>
        /// The Array where the freshly created checks are stored in
        /// </summary>
        public Check[] Checks { get; set; }
    }
}
