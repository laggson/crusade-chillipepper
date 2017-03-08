using System;

namespace FWA.Core.Mvvm
{
   /// <summary>
   /// Stellt eine abgefangene Ausnahme dar, die im MainWindow angezeigt werden soll.
   /// </summary>
   public class ErrorMessage : NotifyUserMessage
   {
      /// <summary>
      /// Die Ausnahme, die übergeben werden soll.
      /// </summary>
      public Exception Exception { get; set; }

      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="ErrorMessage"/>-Klasse.
      /// </summary>
      /// <param name="e"></param>
      /// <param name="message"></param>
      /// <param name="header"></param>
      public ErrorMessage(Exception e, string message = "", string header = "") : base (message, header)
      {
         Exception = e;
      }
   }
}
