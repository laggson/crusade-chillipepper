using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für FWControl.xaml
    /// </summary>
    public partial class FWControl : UserControl
    {
        public FWControl()
        {
            InitializeComponent();
        }

        private void Table_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Header = ((System.ComponentModel.PropertyDescriptor)e.PropertyDescriptor).DisplayName;
        }
    }
}
