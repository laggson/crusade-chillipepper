using FWA.Logic;


namespace FWA.Gui.Content
{
    /// <summary>
    /// This small Windows shows up when the user presses the "Login" button,
    /// but basically does not much code.
    /// </summary>
    public partial class Login
    {
        MainWindow _main;

        public Login(MainWindow main)
        {
            InitializeComponent();
            _main = main;
            this.Owner = _main;
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
        }

        private void BtnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string name = TxtName.Text;
            string pw = TxtPassword.Password;

            //The following is only called, if both textboxes are not empty
            if (!string.Empty.Equals(name) && !string.Empty.Equals(pw))
            {
                if(_main.Control.DBHandler.UserDataCorrect(name, pw))
                {
                    // Write the name of the new user into the button and
                    // Get all the devices from the database, which are filled
                    // into the tables
                    _main.RefreshLoginName();
                    _main.PullDeviceData();
                    this.Close();
                }
                else _main.MsgBox("Fehler", "Die Nutzerdaten waren ungültig.");
            }
        }

        private void Login_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // If the user presses enter, the Button is called.
            // Not necessary, but very nice to have

            if (e.Key == System.Windows.Input.Key.Enter)
                this.BtnLogin_Click(null, null);
        }

        private void Login_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // After the frame is opened, TxtName automatically gets the focus. For easier use.
            TxtName.Focus();
        }
    }
}
