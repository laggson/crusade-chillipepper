using FWA.Gui.Content;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FWA.Gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<TabItem> _tabItems;

        public MainWindow()
        {
            InitializeComponent();
            _tabItems = new List<TabItem>();
            mainMenu.ItemsSource = _tabItems;
            this.AddTab("TLF 3000", new TLF());
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Title = "FWAdministration v" + ver.Major + "." + ver.Minor + "." + ver.Revision;
        }

        private void AddTab(string header, UserControl content)
        {
            TabItem tab = new TabItem();
            tab.Header = header;
            tab.Content = content;
            _tabItems.Add(tab);
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login();
            log.Show();
        }

        private void BtnMail_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto://markus.schmidt98@outlook.de");
        }
    }
}
