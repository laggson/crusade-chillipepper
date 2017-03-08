using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.Mvvm
{
   /// <summary>
   /// Stellt die Basisklasse für eine Nachricht dar, die dem Nutzer eine Mitteilung macht.
   /// </summary>
   public class NotifyUserMessage : MessageBase
   {
      /// <summary>
      /// Ruft die Kopfzeile der anzuzeigenden Meldung ab, oder legt diese fest.
      /// </summary>
      public string Header { get; set; }

      /// <summary>
      /// Ruft die Meldung der anzuzeigenden Meldung ab, oder legt diese fest.
      /// </summary>
      public string Message { get; set; }

      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="NotifyUserMessage"/>-Klasse.
      /// </summary>
      /// <param name="message"></param>
      /// <param name="header"></param>
      internal NotifyUserMessage(string message, string header = "")
      {
         Message = message;
         Header = string.IsNullOrEmpty(header) ? "Fehler" : header;
      }
   }
}
