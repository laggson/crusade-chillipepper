using FWA.Gui.Content;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;
using CControl = FWA.Logic.Control;
namespace FWA.Gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private List<TabItem> _tabItems;

        private void AddTab(string header, UserControl content)
        {
            TabItem tab = new TabItem();
            tab.Header = header;
            tab.Content = content;
            _tabItems.Add(tab);
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login(this);
            log.Show();
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {
            //FWA.Logic.Control.DBHandler.SerializeUser("Ad3", "m.estrich98@gmail.com", "test3");
            System.Diagnostics.Process.Start("mailto://markus.schmidt98@outlook.de");
        }

        public CControl Control
        {
            get; set;
        }

        public MainWindow()
        {
            InitializeComponent();
            _tabItems = new List<TabItem>();
            mainMenu.ItemsSource = _tabItems;
            Control = new CControl();
            this.AddTab("TLF 3000", new TLF());
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "FWAdministration v" + Control.GetVersion();
        }

        public async void MsgBox(string header, string message)
        {
            await this.ShowMessageAsync(header, message);
        }

        public void SetLoginName()
        {
            TxtLogin.Text = "Angemeldet als " + Control?.ConnectedUser.Name;
        }
    }
}
