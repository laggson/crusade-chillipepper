using FWA.Core.Models;
using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.Mvvm
{
   /// <summary>
   /// Eine Nachricht, die das Öffnen des angegebenen <see cref="Models.Dialog"/> bewirkt.
   /// </summary>
   public class RequestDialogOpenMessage : MessageBase
   {
      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="RequestDialogOpenMessage"/>-Klasse.
      /// </summary>
      /// <param name="dlg"></param>
      /// <param name="data"></param>
      public RequestDialogOpenMessage(Dialog dlg, object data = null)
      {
         Dialog = dlg;
         Data = data;
      }

      /// <summary>
      /// Ein beliebiges Datenobjekt, das dem Dialog übergeben werden muss.
      /// </summary>
      public object Data { get; set; }

      /// <summary>
      /// Der Dialog, der geöffnet werden soll. Erfordert Erweiterung der MainWindow-Methode.
      /// </summary>
      public Dialog Dialog { get; set; }
   }
}
