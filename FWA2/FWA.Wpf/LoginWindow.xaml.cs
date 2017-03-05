using System.Windows;
using System.Windows.Input;

namespace FWA.Wpf
{
   /// <summary>
   /// Interaktionslogik für LoginWindow.xaml
   /// </summary>
   public partial class LoginWindow
   {
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

      private void LoginAbgebrochen()
      {
         MessageBox.Show(this, "Das Programm wird jetzt beendet.", "Login abgebrochen", MessageBoxButton.OK, MessageBoxImage.Asterisk);
         System.Environment.Exit(0);
      }

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
