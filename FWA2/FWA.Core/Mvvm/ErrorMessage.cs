using System;

namespace FWA.Core.Mvvm
{
   public class ErrorMessage : NotifyUserMessage
   {
      public Exception Exception { get; set; }

      public ErrorMessage(Exception e, string message = "", string header = "") : base (message, header)
      {
         Exception = e;
      }
   }
}
