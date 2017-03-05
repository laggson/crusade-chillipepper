using FWA.Core.Models;
using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.Mvvm
{
   public class RequestDialogOpenMessage : MessageBase
   {
      public RequestDialogOpenMessage(Dialog dlg, object data = null)
      {
         Dialog = dlg;
         Data = data;
      }

      public object Data { get; set; }
      public Dialog Dialog { get; set; }
   }
}
