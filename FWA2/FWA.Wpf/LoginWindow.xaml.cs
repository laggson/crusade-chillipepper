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
         var result = ViewModel.Login(TxtName.Text, TxtPassword.Password);

         if (result)
            Close();
         // Was mach ich jetzt damit? :D
      }
   }
}
