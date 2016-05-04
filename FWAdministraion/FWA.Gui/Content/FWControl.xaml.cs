using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für FWControl.xaml
    /// </summary>
    public partial class FWControl : UserControl
    {
        MainWindow _main;

        public FWControl(MainWindow main)
        {
            InitializeComponent();
            _main = main;
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
            // TODO: Öffnet neues Fenster, wo die Items aufgelistet sind die mehrfach da sind
            // Bsp: Ein Verkehrsleitkegel wird angezeigt, bei doppeklick sieht man liste mit allen 10
        }

        private void Table_OnEditEnded(object sender, DataGridRowEditEndingEventArgs e)
        {
            // TODO: Push the modified data to DB
        }
    }
}
