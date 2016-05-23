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
        /// <summary>
        /// This static method is used to create a new instance of the LoginWindow by handing over the owner window
        /// and waits, until a user presses the login button, before closing the window and returning the result
        /// </summary>
        /// <param name="owner">Probably the main window</param>
        /// <returns></returns>
        public static LoginWindowResult RequestLogin(Window owner)
        {
            var window = Application.Current.Dispatcher.Invoke(() => new LoginWindow(owner));
            var result = window.WaitForLoginClick();
            Application.Current.Dispatcher.Invoke(() => window.Close());
            return result;
        }

        private readonly ManualResetEvent resetEvent = new ManualResetEvent(false);
        private LoginWindowResult result;

        /// <summary>
        /// You know what a constructor does, don't you?
        /// JK. Initializes some stuff, sets the owner to the handed over window and opens the window on the center of its owner
        /// </summary>
        /// <param name="owner">I wonder.. what window could this be?...</param>
        private LoginWindow(Window owner)
        {
            InitializeComponent();
            this.Owner = owner;
            this.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Show();
        }

        /// <summary>
        /// Waits for the resetEvent to be changed on click of the login button before returning the result of the current login try
        /// </summary>
        /// <returns></returns>
        public LoginWindowResult WaitForLoginClick()
        {
            resetEvent.WaitOne();
            return result;
        }

        /// <summary>
        /// If the text of both TxtBoxes is not empty, the global LoginWindowResult is set and the resetEvent is started to make the assigned task continue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string name = "hs";
            string pw = "password";

            //string name = TxtName.Text;
            //string pw = TxtPassword.Password;

            //The following is only called, if both textboxes are not empty
            if (!string.Empty.Equals(name) && !string.Empty.Equals(pw))
            {
                result = new LoginWindowResult { Username = name, Password = Encoding.UTF8.GetBytes(pw) };
                resetEvent.Set();
            }
        }

        /// <summary>
        /// If the enter key was pressed, BtnLogin_Click is called
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // If the user presses enter, the Button is called.
            // Not necessary, but very nice to have

            if (e.Key == System.Windows.Input.Key.Enter)
                this.BtnLogin_Click(null, null);
        }

        /// <summary>
        /// After opening the window, the TxtName receives the focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            // After the frame is opened, TxtName automatically gets the focus. For easier use.
            TxtName.Focus();
        }
    }

    /// <summary>
    /// A small help class containing the entered user credentials for checking if they're correct.
    /// </summary>
    public class LoginWindowResult
    {
        /// <summary>
        /// The entered username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// The entered password
        /// </summary>
        public byte[] Password { get; set; }
    }
}
