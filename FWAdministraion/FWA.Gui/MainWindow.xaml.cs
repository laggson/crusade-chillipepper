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
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Eine Instanz der <see cref="DBAuthentication"/>. Ist diese null, ist aktuell kein Nutzer angemeldet
        /// </summary>
        public DBAuthentication Database { get; set; }
        /// <summary>
        /// Der aktuell verbundene Benutzer, zurückgegeben aus <see cref="Database"/>
        /// </summary>
        public User CurrentUser { get { return Database?.CurrentUser; } }
        
        /// <summary>
        /// Speichert die alle Kategorien, in die die Gegenstände der Datenbank einsortiert werden
        /// </summary>
        public readonly DeviceCategory[] Categorys =
        {
            new DeviceCategory("TLF", "__TF%"),
            new DeviceCategory("LF","__LF%"),
            //new DeviceCategory("MTF", "__MF%"), // Will be added later
            new DeviceCategory("Halle", string.Empty)
        };

        /// <summary>
        /// Gibt alle aktuell in <see cref="mainMenu"/> vorhandenen Instanzen von <see cref="OverviewControl"/> zurück
        /// </summary>
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

        /// <summary>
        /// Erstellt eine neue Instanz des <see cref="MainWindow"/> und generiert die Tabs aus <see cref="Categorys"/>
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            mainMenu.ItemsSource = Categorys.Select(x =>
                new TabItem
                {
                    Width = (this.Width -20) / Categorys.Length,
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

        private void AlertNoDatabaseConnection()
        {
            MsgBox("Für diese Aktion muss eine aktive Datenbankverbindung bestehen", "Keine Verbindung");
        }

        /// <summary>
        /// Macht ganz schön kranken scheiß. Muss ich mir in Ruhe mal anschauen und refactorn
        /// </summary>
        /// <param name="device"></param>
        /// <param name="name"></param>
        /// <param name="db"></param>
        public void Test(Device device, string name, DBAuthentication db)
        {
            var test = mainMenu.ItemsSource as List<TabItem>;
            var tab = new TabItem();
            tab.Header = "Test " + device.Name;
            test.Add(tab);

            foreach(TabItem widthChangingTabItem in test)
            {
                widthChangingTabItem.Width = (this.Width - 20) / test.Count;
            }

            mainMenu.ItemsSource = null;
            mainMenu.ItemsSource = test;
            //mainMenu erhält neuen Tab mit namen 'Test ' + device.Name;

            //neues CheckControl mit Event wenn fertig
            var controller = new CheckControl();
            controller.ChecksFinished += Controller_ChecksFinished;

            //Alle Devices mit dem Namen aus DB
            var devices = GetDevicesByTabName(name, db).Where(x => x.Name.Equals(device.Name));
            var checks = new List<Check>();

            //Alle neu geladenen Devices in Liste mit Checks umwandeln; Datum, Tester, Check -> Ok wird automatisch gesetzt
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

            //Die neue Liste wird zur Itemssource des neuen CheckControl
            controller.Table.ItemsSource = checks;
            tab.Content = controller;
            //CheckControl wird in neuen Tab eingefügt und als aktueller Tab gesetzt
            mainMenu.SelectedItem = tab;
        }

        private List<Device> GetDevicesByTabName(string name, DBAuthentication db)
        {
            string invNumberLike = Categorys.First(x => name.Equals(x.DisplayName))?.InvNumberLike;

            if (invNumberLike == null)
                return new List<Device>();

            return db.GetDevicesByInvNumberType(invNumberLike);
        }

        /// <summary>
        /// Der Tab mit dem übergebenen HashCode wird geschlossen
        /// </summary>
        /// <param name="contentHash">Der HashCode des zu schließenden Tabs</param>
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

        /// <summary>
        /// Öffnet ein asynchrones Dialog Fenster mit den angegebenen Daten. Muss mit 'await Dispatcher.Invoke(() => MsgBox(...))' aufgerufen werden
        /// </summary>
        /// <param name="header">Die Überschrift des Dialogs</param>
        /// <param name="message">Die Nachricht</param>
        /// <param name="style">Die Anzahl der anzuzeigenen Buttons</param>
        /// <returns>Das Ergebnis der vom Nutzer gewählten Aktion</returns>
        public MessageBoxResult MsgBox(string message, string header = "", MessageBoxButton style = MessageBoxButton.OK)
        {
            return MessageBox.Show(message, header, style);
        }
        
        /// <summary>
        /// Ist ein Nutzer angemeldet, wird dessen Name in <see cref="TxtLogin"/> geschrieben, andernfalls wird 'Nicht angemeldet' eingefügt
        /// </summary>
        public void RefreshLoginName()
        {
            Dispatcher.Invoke(() => TxtLogin.Header = "Verbunden: " + CurrentUser?.Name ?? "Verbinden: Nicht verbunden");
        }

        private void Login()
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
                MsgBox(string.Format("Fehler beim Einloggen: {0}{1}", Environment.NewLine, ex.Message), "Warnung");
            }
        }

        private void Logout()
        {
            string msg = "Sind sie sicher, dass sie den Benutzer " + CurrentUser?.Name + " abmelden wollen";

            Dispatcher.Invoke(() =>
            {
                var result = this.MsgBox(msg, "Warnung", MessageBoxButton.YesNo);
                
                if (result == MessageBoxResult.Yes)
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
                MsgBox("Logout before you login", "Existing Login found");
            }
            else
            {
                Task.Run(new Action(Login));
            }
        }

        private void ButtonLogout_Click(object sender, RoutedEventArgs e)
        {
            if (Database == null)
            {
                MsgBox("Login to logout", "No Login found");
            }
            else
            {
                Task.Run(new Action(Logout));
            }
        }

        private void ButtonMail_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("mailto://markus.schmidt98@outlook.de");
        }
        
        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "FWAdministration v" + AwkwardFlyingClassInBackground.Version;
        }

        #endregion

        #region Key-Pressed-Area
        
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.Tab) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                    this.LowerMenuIndex();
                else this.RaiseMenuIndex();
            }
            else if ((Keyboard.IsKeyDown(Key.D1) || Keyboard.IsKeyDown(Key.NumPad1)) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                mainMenu.SelectedIndex = 0;
            else if ((Keyboard.IsKeyDown(Key.D2) || Keyboard.IsKeyDown(Key.NumPad2)) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                mainMenu.SelectedIndex = 1;
            else if ((Keyboard.IsKeyDown(Key.D3) || Keyboard.IsKeyDown(Key.NumPad3)) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                mainMenu.SelectedIndex = 2;
            else if ((Keyboard.IsKeyDown(Key.D4) || Keyboard.IsKeyDown(Key.NumPad4)) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                mainMenu.SelectedIndex = 3;
        }

        private void LowerMenuIndex()
        {
            mainMenu.SelectedIndex = mainMenu.SelectedIndex == 0 ? mainMenu.Items.Count - 1 : mainMenu.SelectedIndex - 1;
        }

        private void RaiseMenuIndex()
        {
            mainMenu.SelectedIndex = mainMenu.SelectedIndex + 1 % mainMenu.Items.Count;
        }

        #endregion

        private void ButtonAbout_Click(object sender, RoutedEventArgs e)
        {
            MsgBox(new NotImplementedException("Deine Mudda riecht nach Fisch.").Message);
        }
    }
}
