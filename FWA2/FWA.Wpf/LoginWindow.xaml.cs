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

      private void Login_KeyUp(object sender, KeyEventArgs e)
         {
         if (e.Key == Key.Enter)
         {
            TryLogin();
         }
      }

      private void BtnLogin_Click(object sender, RoutedEventArgs e)
      {
         TryLogin();
      }

      private void TryLogin()
      {
         var loginErfolgreich = ViewModel.Login(TxtName.Text, TxtPassword.Password);

         if (loginErfolgreich)
         {
            Close();
         }
         else
         {
            LoginAbgebrochen();
         }
      }

      private void LoginAbgebrochen()
      {
         MessageBox.Show(this, "Das Programm wird jetzt beendet.", "Login abgebrochen", MessageBoxButton.OK, MessageBoxImage.Information);
      }

      private void BtnAbort_Click(object sender, RoutedEventArgs e)
      {
         LoginAbgebrochen();
      }
   }
}
