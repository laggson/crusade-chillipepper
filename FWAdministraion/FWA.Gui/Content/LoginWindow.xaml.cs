using System.Text;
using System.Threading;
using System.Windows;

namespace FWA.Gui.Content
{
    /// <summary>
    /// This small Windows shows up when the user presses the "Login" button,
    /// but basically does not much code.
    /// </summary>
    public partial class LoginWindow
    {
        public static LoginWindowResult RequestLogin(Window owner)
        {
            var window = Application.Current.Dispatcher.Invoke(() => new LoginWindow(owner));
            var result = window.WaitForLoginClick();
            Application.Current.Dispatcher.Invoke(() => window.Close());
            return result;
        }

        private readonly ManualResetEvent resetEvent = new ManualResetEvent(false);
        private LoginWindowResult result;

        private LoginWindow(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Show();
        }

        public LoginWindowResult WaitForLoginClick()
        {
            resetEvent.WaitOne();
            return result;
        }

        private void BtnLogin_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string name = TxtName.Text;
            string pw = TxtPassword.Password;

            //The following is only called, if both textboxes are not empty
            if (!string.Empty.Equals(name) && !string.Empty.Equals(pw))
            {
                result = new LoginWindowResult { Username = name, Password = Encoding.UTF8.GetBytes(pw) };
                resetEvent.Set();
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

    public class LoginWindowResult
    {
        public string Username { get; set; }
        public byte[] Password { get; set; }
    }
}
