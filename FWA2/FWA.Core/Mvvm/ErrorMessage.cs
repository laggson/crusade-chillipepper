using GalaSoft.MvvmLight.Messaging;
using System;

namespace FWA.Core.Mvvm
{
   public class ErrorMessage : MessageBase
   {
      public Exception Exception { get; set; }
      public string Header { get; set; }
      public string Message { get; set; }

      public ErrorMessage(Exception e, string message = "", string header = "")
      {
         Exception = e;
         Message = string.IsNullOrEmpty(message) ? e.Message : message;
         Header = string.IsNullOrEmpty(header) ? "Fehler" : header;
      }
   }
}
