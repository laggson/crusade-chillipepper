using FWA.Core.Models;
using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.Mvvm
{
   /// <summary>
   /// Stellt die Antwort auf eine im MainWindow angezeigte Nachricht dar, die vom Sender verarbeitet werden kann.
   /// </summary>
   public class MessageboxResponseMessage : MessageBase
   {
      public MyDialogResult Result { get; set; }
      public string Kommentar { get; set; }

      public MessageboxResponseMessage(MyDialogResult result, string kommentar = "")
      {
         Result = result;
         Kommentar = kommentar;
      }
   }
}
