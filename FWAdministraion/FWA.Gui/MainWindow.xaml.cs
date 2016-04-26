using System;
using System.Windows;

namespace FWA.Gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Title = "FWAdministration v" + ver.Major + "." + ver.Minor + "." + ver.Revision;
        }
    }
}
