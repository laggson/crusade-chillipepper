#define Debug
using MahApps.Metro.Controls.Dialogs;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using FWA.Gui.Content;
using System.Windows;
using System;
using System.Linq;
using FWA.Logic.Storage;
using System.Threading.Tasks;
using FWA.Logic;
using FWA.Gui.Logic;
using FWA.Logic.Exceptions;

namespace FWA.Gui
{
    /// <summary>
    /// This is the main window (o_O) and therefor does some stuff a Main Window does. Is created as main class of the Gui package.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Stores the DBAuthentication object, which is used for checking, if there's any user connected
        /// (If not, there's not much happening in here)
        /// </summary>
        public DBAuthentication Database { get; set; }

        /// <summary>
        /// Returns the User object that is stored in the Database object
        /// </summary>
        public User CurrentUser { get { return Database?.CurrentUser; } }

        /// <summary>
        /// 
        /// </summary>
        public readonly DeviceCategory[] Categorys =
        {
            new DeviceCategory("TLF", "__TF%"),
            new DeviceCategory("LF","__LF%"),
            //new DeviceCategory("MTF", "__MF%"),
            new DeviceCategory("Halle", string.Empty)
        };

        public OverviewControl[] Tabs
        {
            get
            {
                return Dispatcher.Invoke(() =>
                    mainMenu.ItemsSource.
                    OfType<TabItem>().
                    Select(t => t.Content).
                    OfType<OverviewControl>().
                    ToArray());
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            mainMenu.ItemsSource = Categorys.Select(x =>
                new TabItem
                {
                    Header = x.DisplayName,
                    Content = NewOverviewControl(x)
                }).ToList();
        }

        private OverviewControl NewOverviewControl(DeviceCategory category)
        {
            var result = new OverviewControl(category);
            result.DeviceDoubleClicked += Overview_DeviceDoubleClicked;
            result.DeviceEdited += Overview_DeviceEdited;
            return result;
        }

        private void ExecuteIfDatabaseAvailable(Action<DBAuthentication> action)
        {
            var db = Database;
            if (db == null)
            {
                AlertNoDatabaseConnection();
            }
            else
            {
                action.Invoke(db);
            }
        }

        private async void AlertNoDatabaseConnection()
        {
            await MsgBox("Keine Verbindung", "Für diese Aktion muss eine aktive Datenbankverbindung bestehen");
        }

        public void Test(Device device, string name, DBAuthentication db)
        {
            //The new Tab is created, named and added to the ItemsSource, which has to be assigned again.
            var test = mainMenu.ItemsSource as List<TabItem>;
            var tab = new TabItem();
            tab.Header = "Test " + device.Name;
            test.Add(tab);
            mainMenu.ItemsSource = null;
            mainMenu.ItemsSource = test;

            //The DeviceCheck is initialized and gets the data, filtered by name, as Itemssource
            var controller = new CheckControl();
            controller.ChecksFinished += Controller_ChecksFinished;
            var devices = GetDevicesByTabName(name, db).Where(x => x.Name.Equals(device.Name));
            var checks = new List<Check>();

            foreach (Device d in devices)
            {
                checks.Add(new Check
                {
                    Device = d,
                    DateChecked = DateTime.Now,
                    Tester = CurrentUser,
                    CheckType = CheckType.OK
                });
            }

            controller.Table.ItemsSource = checks;
            tab.Content = controller;
            mainMenu.SelectedItem = tab;
        }

        private List<Device> GetDevicesByTabName(string name, DBAuthentication db)
        {
            string invNumberLike = Categorys.First(x => name.Equals(x.DisplayName))?.InvNumberLike;

            if (invNumberLike == null)
                return new List<Device>();

            return db.GetDevicesByInvNumberType(invNumberLike);
        }

        public void CloseTab(int contentHash)
        {
            var tabs = mainMenu.ItemsSource as List<TabItem>;

            foreach (TabItem tab in tabs)
            {
                if (tab.Content.GetHashCode() == contentHash)
                {
                    mainMenu.ItemsSource = null;
                    tabs.Remove(tab);
                    mainMenu.ItemsSource = tabs;
                    return;
                }
            }
        }

        public async Task<MessageDialogResult> MsgBox(string header, string message, MessageDialogStyle style = MessageDialogStyle.Affirmative)
        {
            // Task.Run(() => MaterialDesignThemes.Wpf.DialogHost.Show(string.Format("{0}:{1}{2}", header, Environment.NewLine, message)));
            var dialog = await this.ShowMessageAsync(header, message, style);
            return dialog;
        }

        /// <summary>
        /// Retrieves the name of the connected User from the control class and writes it into the Login button text.
        /// </summary>
        public void RefreshLoginName()
        {
            Dispatcher.Invoke(() => TxtLogin.Text = CurrentUser?.Name ?? "Nicht angemeldet");
        }

        private async void Login()
        {
            try
            {
                var loginResult = LoginWindow.RequestLogin(this);
                var db = new DBAuthentication(loginResult.Username, loginResult.Password);
                Database = db;

                foreach (var overviewControl in Tabs)
                {
                    overviewControl.LoadValuesFromDatabase(db);
                }

                RefreshLoginName();
            }
            catch (AuthenticationException ex)
            {
                Database = null;
                await Dispatcher.Invoke(() => MsgBox("Warnung", string.Format("Fehler beim Einloggen: {0}{1}", Environment.NewLine, ex.Message)));
            }
        }

        private async void Logout()
        {
            string msg = "Sind sie sicher, dass sie den Benutzer " + CurrentUser?.Name + " abmelden wollen";

            await Dispatcher.Invoke(async () =>
            {
                var result = await this.MsgBox("Warnung", msg, MessageDialogStyle.AffirmativeAndNegative);
                
                if (result == MessageDialogResult.Affirmative)
                {
                    foreach (var overviewControl in Tabs)
                    {
                        overviewControl.Clear();
                    }

                    Database = null;
                    RefreshLoginName();
                }
            });
        }

        #region Events

        private void Controller_ChecksFinished(object sender, ChecksFinishedEventArgs e)
        {
            ExecuteIfDatabaseAvailable(db =>
            {
                db.InsertMultiple(e.Checks);
                CloseTab(sender.GetHashCode());
            });
        }

        private void Overview_DeviceEdited(object sender, DeviceEventArgs e)
        {
            ExecuteIfDatabaseAvailable(db => db.Insert(e.Device));
        }

        private void Overview_DeviceDoubleClicked(object sender, DeviceEventArgs e)
        {
            ExecuteIfDatabaseAvailable(db =>
            {
                Dispatcher.Invoke(() => Test(e.Device, e.ControlName, db));
            });
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Database != null)
            {
                Task.Run(new Action(Logout));
                
            }
            else
            {
                Task.Run(new Action(Login));
            }
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
            this.Title = "FWAdministration v" + AwkwardFlyingClassInBackground.GetVersion();
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
            mainMenu.SelectedIndex = mainMenu.SelectedIndex == 0 ? mainMenu.Items.Count - 1 : mainMenu.SelectedIndex - 1;
        }

        /// <summary>
        /// The currently selected index of the dragablz menu is raised by 1, or is set to 0 if it is the highest
        /// </summary>
        private void RaiseMenuIndex()
        {
            mainMenu.SelectedIndex = mainMenu.SelectedIndex + 1 % mainMenu.Items.Count;
        }

        #endregion
    }
}
