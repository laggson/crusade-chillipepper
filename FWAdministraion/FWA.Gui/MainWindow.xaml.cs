#define Debug
using MahApps.Metro.Controls.Dialogs;
using CControl = FWA.Logic.Control;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using FWA.Gui.Content;
using System.Windows;
using System;
using System.Linq;
using System.Diagnostics;
using FWA.Logic.Storage;
using System.Collections;

namespace FWA.Gui
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Dictionary<string, OverviewControl> _tabs = new Dictionary<string, OverviewControl>();

        public MainWindow()
        {
            InitializeComponent();
            Control = new CControl();
            Control.TFDataChanged += Control_TFDataChanged;
            Control.LFDataChanged += Control_LFDataChanged;
            Control.MFDataChanged += Control_MFDataChanged;
            Control.HallDataChanged += Control_HallDataChanged;

            Tabs.Add("TLF", new OverviewControl(this, "TLF"));
            Tabs.Add("LF", new OverviewControl(this, "LF"));
            //Tabs.Add("MTF", new FWControl(this, "MTF"));
            Tabs.Add("Halle", new OverviewControl(this, "Halle"));
            this.SetItemssource();

        }

        public CControl Control
        {
            get; set;
        }

        public void Test(Device device, string name)
        {
            //The new Tab is created, named and added to the ItemsSource, which has to be assigned again.
            List<TabItem> test = mainMenu.ItemsSource as List<TabItem>;
            TabItem tab = new TabItem();
            tab.Header = "Test " + device.Name;
            test.Add(tab);
            mainMenu.ItemsSource = null;
            mainMenu.ItemsSource = test;

            //The DeviceCheck is initialized and gets the data, filtered by name, as Itemssource
            CheckControl controller = new CheckControl(this);
            List<Device> devices = Control.DevicesByName(device.Name, Control.DataListByName(name));
            List<Check> checks = new List<Check>();

            foreach(Device d in devices)
            {
                checks.Add(new Check(d)
                {
                    //Name and InvNumber set automatically in Constructor
                    ID = d.ID,
                    DateChecked = DateTime.Now,
                    WhoChecked = Control.ConnectedUser,
                    CheckType = CheckType.OK
                });
            }
            
            controller.Table.ItemsSource = checks;
            tab.Content = controller;
            mainMenu.SelectedItem = tab;
        }

        public void CloseTab(int hash)
        {
            List<TabItem> tabs = mainMenu.ItemsSource as List<TabItem>;

            foreach(TabItem tab in tabs)
            {
                if(tab.Content.GetHashCode() == hash)
                {
                    mainMenu.ItemsSource = null;
                    tabs.Remove(tab);
                    mainMenu.ItemsSource = tabs;
                    return;
                }
            }
        }

        private void SetItemssource()
        {
            List<TabItem> test = new List<TabItem>();

            foreach (var v in Tabs)
            {
                TabItem t = new TabItem();
                t.Header = v.Key;
                t.Content = v.Value;
                test.Add(t);
            }
            mainMenu.ItemsSource = test;
        }

        public async void MsgBox(string header, string message)
        {
            //await this.ShowMessageAsync(header, message);
            await MaterialDesignThemes.Wpf.DialogHost.Show(message);
        }

        /// <summary>
        /// Retrieves the name of the connected User from the control class and writes it into the Login button text.
        /// </summary>
        public void RefreshLoginName()
        {
            if (Control.ConnectedUser != null)
                TxtLogin.Text = "Angemeldet als " + Control.ConnectedUser?.Name;
            else TxtLogin.Text = "Nicht angemeldet";
        }

        public Dictionary<string, OverviewControl> Tabs
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

        #region Events

        private void Control_TFDataChanged(object sender, EventArgs e)
        {
            OverviewControl usedTab;
            Tabs.TryGetValue("TLF", out usedTab);
            usedTab.Table.ItemsSource = Control.TrimList(Control.TFData);
        }

        private void Control_LFDataChanged(object sender, EventArgs e)
        {
            OverviewControl usedTab;
            Tabs.TryGetValue("LF", out usedTab);
            usedTab.Table.ItemsSource = Control.TrimList(Control.LFData);
        }

        private void Control_MFDataChanged(object sender, EventArgs e)
        {
            OverviewControl usedTab;
            Tabs.TryGetValue("MTF", out usedTab);
            if (usedTab != null)
                usedTab.Table.ItemsSource = Control.MFData;
        }

        private void Control_HallDataChanged(object sender, EventArgs e)
        {
            OverviewControl usedTab;
            Tabs.TryGetValue("Halle", out usedTab);
            usedTab.Table.ItemsSource = Control.HallData;
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Control.ConnectedUser != null)
            {
                this.Logout();
            }
            else
            {
                LoginWindow log = new LoginWindow(this);
                log.Show();
            }

        }

        private async void Logout()
        {
            string msg = "Sind sie sicher, dass sie den Benutzer " + Control.ConnectedUser?.Name + " abmelden wollen";
            var result = await this.ShowMessageAsync("Warnung", msg, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {

                OverviewControl usedTab;
                Tabs.TryGetValue("TLF", out usedTab);
                usedTab.Table.ItemsSource = null;

                Tabs.TryGetValue("LF", out usedTab);
                usedTab.Table.ItemsSource = null;

                Tabs.TryGetValue("MTF", out usedTab);
                if(usedTab != null)
                usedTab.Table.ItemsSource = null;

                Tabs.TryGetValue("Halle", out usedTab);
                usedTab.Table.ItemsSource = null;

                Control.ConnectedUser = null;
                this.RefreshLoginName();
            }
        }

        public void logintemptestzeug()
        {

#if Debug
            Control.ConnectedUser = new User()
            {
                ID = 0,
                Name = "hs",
                EMail = "hermann.schmidt24@freenet.de",
                Hash = "$2a$06$T0lScnYf7KXijnGzshe74OHrpO5nJap3XwFaKTVBZIBzVX5ZnlRgu",
                Salt = "$2a$06$T0lScnYf7KXijnGzshe74O",
                AccountType = FWA.Logic.Storage.AccountType.User
            };
            this.RefreshLoginName();
            Control.DBHandler.GetAllDeviceData();
#endif
        }

        /// <summary>
        /// Opens your local E-Mail software to write a report letter.
        /// Will probably be replaced later on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto://markus.schmidt98@outlook.de");
        }

        /// <summary>
        /// Fired when the frame is loaded. Retrieves the Application version from the
        /// Control class and writes it into the window title.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "FWAdministration v" + Control.GetVersion();
            //this.logintemptestzeug();
        }

        #endregion

        #region Key-Pressed-Area

        /// <summary>
        /// Reacts to any key pressed and is supposed to process them.
        /// Does not require to be fired with any filled parameters,
        /// because it uses the static "Keyboard.IsKeyDown" method
        /// </summary>
        /// <param name="sender">The object that caused the event call</param>
        /// <param name="e">The arguments concerning the pressed key, etc</param>
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) && Keyboard.IsKeyDown(Key.Tab))
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    this.LowerMenuIndex();
                else this.RaiseMenuIndex();
            }
        }

        /// <summary>
        /// The currently selected index of the dragablz menu is lowered by 1, or set to the highest if it is 0
        /// </summary>
        private void LowerMenuIndex()
        {
            if (mainMenu.SelectedIndex == 0)
            {
                mainMenu.SelectedIndex = mainMenu.Items.Count - 1;
            }
        }

        /// <summary>
        /// The currently selected index of the dragablz menu is raised by 1, or is set to 0 if it is the highest
        /// </summary>
        private void RaiseMenuIndex()
        {
            if (mainMenu.SelectedIndex == mainMenu.Items.Count - 1)
            {
                mainMenu.SelectedIndex = 0;
            }
        }

        #endregion
    }
}
