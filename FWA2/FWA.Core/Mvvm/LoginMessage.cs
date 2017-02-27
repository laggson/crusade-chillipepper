namespace FWA.Core.Mvvm
{
   public class LoginMessage : GalaSoft.MvvmLight.Messaging.MessageBase
   {
      public LoginMessage(bool jetztEingeloggt)
      {
         JetztEingeloggt = jetztEingeloggt;
      }

      /// <summary>
      /// Gibt an, ob der neue Status 'angemeldet' ist. Sonst natürlich abgemeldet.
      /// </summary>
      public bool JetztEingeloggt { get; set; }
   }
}
