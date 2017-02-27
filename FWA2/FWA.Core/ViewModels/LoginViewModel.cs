using FWA.Core.Exceptions;
using FWA.Core.Helpers;
using FWA.Core.Models;
using FWA.Core.Mvvm;
using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.ViewModels
{
   public class LoginViewModel : ObservableObject
   {
      public LoginViewModel()
      {
         DBAuthentication.Dispose();
      }

      public bool Login(string username, string pass)
      {
         var bytes = System.Text.Encoding.UTF8.GetBytes(pass);

         try
         {
            DBAuthentication.Create(username, bytes);
         }
         catch (AuthenticationException e)
         {
            Messenger.Default.Send(new ErrorMessage(e));
            return false;
         }

         // TODO: Irgendwem bescheid geben, dass alles bereit ist.
         //Messenger.Default.Send(new DialogMessage(Dialog.LoginWindow, DialogStatus.Fertig));
         Messenger.Default.Send(new LoginMessage(true));
         return true;
      }
   }
}
