using System.Windows;
using System.Windows.Input;

namespace FWA.Wpf
{
   /// <summary>
   /// Stellt das Anmelde-Fenster dar.
   /// </summary>
   public partial class LoginWindow
   {
      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="LoginWindow"/>-Klasse.
      /// </summary>
      public LoginWindow()
      {
         InitializeComponent();
         TxtName.Focus();

      }

      #region Events
      private bool erfolgreichEingeloggt;

      private void BtnAbort_Click(object sender, RoutedEventArgs e)
      {
         LoginAbgebrochen();
      }

      private void BtnLogin_Click(object sender, RoutedEventArgs e)
      {
#if DEBUG
         TxtName.Text = "hs";
         TxtPassword.Password = "Vivendi2016";
#endif
         TryLogin();
      }

      private void Login_KeyUp(object sender, KeyEventArgs e)
      {
         if (TxtError.Visibility == Visibility.Visible)
         {
            TxtError.Visibility = Visibility.Collapsed;
         }
         else if (e.Key == Key.Enter)
         {
            TryLogin();
         }
      }

      private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
      {
         if (!erfolgreichEingeloggt)
            LoginAbgebrochen();
      }

      #endregion

      /// <summary>
      /// Zeigt eine Nachricht über das beenden des Programms und beendet das Programm. :D
      /// </summary>
      private void LoginAbgebrochen()
      {
         MessageBox.Show(this, "Das Programm wird jetzt beendet.", "Login abgebrochen", MessageBoxButton.OK, MessageBoxImage.Asterisk);
         System.Environment.Exit(0);
      }

      /// <summary>
      /// Versucht, sich mit den angegebenen Daten an der Datenbank anzumelden, und zeigt bei einem Fehler eine Meldung.
      /// </summary>
      private void TryLogin()
      {
         TxtError.Visibility = Visibility.Hidden;

         var loginErfolgreich = ViewModel.Login(TxtName.Text, TxtPassword.Password);

         if (loginErfolgreich)
         {
            erfolgreichEingeloggt = true;
            Close();
         }
         else
         {
            TxtError.Visibility = Visibility.Visible;
         }
      }
   }
}
