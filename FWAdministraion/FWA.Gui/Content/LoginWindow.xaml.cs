using System.Text;
using System.Threading;
using System.Windows;

namespace FWA.Gui.Content
{
    public partial class LoginWindow
    {
        /// <summary>
        /// Erstellt ein neues <see cref="LoginWindow"/> und wartet, bis es vom Nutzer geschlossen wird, um die eingegebenen Daten an <paramref name="owner"/> zurückzugeben
        /// </summary>
        /// <param name="owner">Das Fenster, das die Methode aufgerufen hat</param>
        /// <returns>Die vom Benutzer eingegebenen Daten</returns>
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
        
        /// <summary>
        /// Blockiert den Thread der <see cref="LoginWindow.RequestLogin(Window)"/>-Methode, 
        /// bis das resetEvent gestartet wird, mit dem das Fenster geschlossen wird
        /// </summary>
        /// <returns></returns>
        public LoginWindowResult WaitForLoginClick()
        {
            resetEvent.WaitOne();
            return result;
        }
        
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string name = "hs";
            string pw = "password";

            if (!string.Empty.Equals(name) && !string.Empty.Equals(pw))
            {
                result = new LoginWindowResult { Username = name, Password = Encoding.UTF8.GetBytes(pw) };
                resetEvent.Set();
            }
        }
        
        private void Login_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
                this.BtnLogin_Click(null, null);
        }
        
        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            TxtName.Focus();
        }
    }
    
    /// <summary>
    /// Die Daten der ausgeführten Anmeldung
    /// </summary>
    public class LoginWindowResult
    {
        /// <summary>
        /// Der Name oder die E-Mail des Nutzers, der versuchte sich anzumelden
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Das eingegebene Passwort. Konvertiert für späteres Hashen
        /// </summary>
        public byte[] Password { get; set; }
    }
}
