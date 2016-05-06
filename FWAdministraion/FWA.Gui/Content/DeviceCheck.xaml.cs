using System.Windows;
using System.Windows.Controls;

namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für DeviceCheck.xaml
    /// </summary>
    public partial class DeviceCheck : UserControl
    {
        TabItem _parent;

        public DeviceCheck(TabItem parent)
        {
            InitializeComponent();
            _parent = parent;
        }

        private void BtnAbort_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
