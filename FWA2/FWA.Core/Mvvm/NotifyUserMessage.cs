using GalaSoft.MvvmLight.Messaging;

namespace FWA.Core.Mvvm
{
   public class NotifyUserMessage : MessageBase
   {
      public string Header { get; set; }
      public string Message { get; set; }

      public NotifyUserMessage(string message, string header = "")
      {
         Message = message;
         Header = string.IsNullOrEmpty(header) ? "Fehler" : header;
      }
   }
}
