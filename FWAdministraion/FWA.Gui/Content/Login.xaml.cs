using FWA.Logic;


namespace FWA.Gui.Content
{
    /// <summary>
    /// Interaktionslogik für Login.xaml
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
            this.CheckData(TxtName.Text, TxtPassword.Password);
        }

        private void CheckData(string name, string pw)
        {
            if (_main.Control.DBHandler.UserDataCorrect(name, pw))
            {
                _main.SetLoginName();
                this.Close();
            }
            else _main.MsgBox("Fehler", "Die Nutzerdaten waren ungültig.");
        }

        private void Login_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                if (!TxtName.Text.Equals(string.Empty) && !TxtPassword.Password.Equals(string.Empty))
                    BtnLogin_Click(null, null);
        }

        private void Login_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            TxtName.Focus();
        }
    }
}
