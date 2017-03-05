using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
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
         if(TxtError.Visibility == Visibility.Visible)
         {
            TxtError.Visibility = Visibility.Collapsed;
         }
         else if (e.Key == Key.Enter)
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
         TxtError.Visibility = Visibility.Hidden;

         var loginErfolgreich = ViewModel.Login(TxtName.Text, TxtPassword.Password);

         if (loginErfolgreich)
         {
            Close();
         }
         else
         {
            TxtError.Visibility = Visibility.Visible;
         }
      }

      private void LoginAbgebrochen()
      {
         //await this.ShowMessageAsync(, );
         MessageBox.Show(this, "Das Programm wird jetzt beendet.", "Login abgebrochen", MessageBoxButton.OK, MessageBoxImage.Asterisk);
         System.Environment.Exit(0);
      }

      private void BtnAbort_Click(object sender, RoutedEventArgs e)
      {
         LoginAbgebrochen();
      }
   }
}
