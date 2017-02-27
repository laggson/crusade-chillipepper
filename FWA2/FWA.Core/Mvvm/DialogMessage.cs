using FWA.Core.Models;
using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.Mvvm
{
   public class RequestDialogOpenMessage : MessageBase
   {
      public RequestDialogOpenMessage(Dialog dlg)
      {
         Dialog = dlg;
      }

      public Dialog Dialog { get; set; }
   }
}
