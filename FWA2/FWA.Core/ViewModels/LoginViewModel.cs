using FWA.Core.Exceptions;
using FWA.Core.Helpers;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.ViewModels
{
   public class LoginViewModel : ObservableObject
   {
      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="LoginViewModel"/>-Klasse
      /// </summary>
      public LoginViewModel()
      {
         DBAuthentication.Dispose();
      }

      /// <summary>
      /// Versucht, sich mit den angegebenen Daten an der Datenbank anzumelden und gibt bei Erfolg true zurück.
      /// </summary>
      /// <param name="username"></param>
      /// <param name="pass"></param>
      /// <returns></returns>
      public bool Login(string username, string pass)
      {
         var bytes = System.Text.Encoding.UTF8.GetBytes(pass);
         //Messenger.Default.Send(new ErrorMessage(new System.Exception(), "Hallo"));
         try
         {
            DBAuthentication.Create(username, bytes);
         }
         catch (AuthenticationException)
         {
            return false;
         }

         // Erfolg. Nachricht senden.
         Messenger.Default.Send(new LoginMessage(true));

         return true;
      }
   }
}
