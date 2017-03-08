namespace FWA.Core.Mvvm
{
   /// <summary>
   /// Stellt dar, dass sich der Anmelde-Status der Datenbank geändert hat.
   /// </summary>
   public class LoginMessage : GalaSoft.MvvmLight.Messaging.MessageBase
   {
      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="LoginMessage"/>-Klasse.
      /// </summary>
      /// <param name="jetztEingeloggt"></param>
      public LoginMessage(bool jetztEingeloggt)
      {
         JetztEingeloggt = jetztEingeloggt;
      }

      /// <summary>
      /// Gibt an, ob durch die Aktion ein Nutzer angemeldet wurde.
      /// </summary>
      public bool JetztEingeloggt { get; set; }
   }
}
