using MahApps.Metro.Controls.Dialogs;
using CControl = FWA.Logic.Control;
using System.Collections.Generic;
using System.Windows.Controls;
using FWA.Gui.Content;
using System.Windows;
using System.Windows.Input;
using System;

namespace FWA.Gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Dictionary<string, FWControl> _tabs = new Dictionary<string, FWControl>();

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Login log = new Login(this);
            log.Show();
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("mailto://markus.schmidt98@outlook.de");

            FWControl ret;
            Tabs.TryGetValue("TLF", out ret);
            ret.Table.ItemsSource = Control.DBHandler.GetTLFData();
        }

        public CControl Control
        {
            get; set;
        }

        public MainWindow()
        {
            InitializeComponent();
            Control = new CControl();

            Tabs.Add("TLF", new FWControl());
            Tabs.Add("LF", new FWControl());
            Tabs.Add("MTF", new FWControl());
            Tabs.Add("Halle", new FWControl());
            this.SetItemssource();
            
        }

        private void SetItemssource()
        {
            List<TabItem> test = new List<TabItem>();

            foreach(var v in Tabs)
            {
                TabItem t = new TabItem();
                t.Header = v.Key;
                t.Content = v.Value;
                test.Add(t);
            }
            mainMenu.ItemsSource = test;
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
            TxtLogin.Text = "Angemeldet als " + Control.ConnectedUser?.Name;
            this.SetData();
        }

        public void SetData()
        {
            FWControl usedTab;
            Tabs.TryGetValue("TLF", out usedTab);
            usedTab.Table.ItemsSource = Control.DBHandler.GetTLFData();

            Tabs.TryGetValue("LF", out usedTab);
            usedTab.Table.ItemsSource = Control.DBHandler.GetLFData();

            Tabs.TryGetValue("MTF", out usedTab);
            usedTab.Table.ItemsSource = Control.DBHandler.GetMTFData();

            Tabs.TryGetValue("Halle", out usedTab);
            usedTab.Table.ItemsSource = Control.DBHandler.GetHallData();
        }

        public Dictionary<string, FWControl> Tabs
        {
            get
            {
                return _tabs;
            }
            set
            {
                _tabs = value;
            }
        }

        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if( (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Tab))
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    this.LowerMenuIndex();
                else this.RaiseMenuIndex();
            }
        }

        private void LowerMenuIndex()
        {
            if(mainMenu.SelectedIndex == 0)
            {
                mainMenu.SelectedIndex = mainMenu.Items.Count - 1;
            }
        }

        private void RaiseMenuIndex()
        {
            if(mainMenu.SelectedIndex == mainMenu.Items.Count -1)
            {
                mainMenu.SelectedIndex = 0;
            }
        }
    }
}
