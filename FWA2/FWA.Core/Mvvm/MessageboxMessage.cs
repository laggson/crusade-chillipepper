using System.Collections;

namespace FWA.Core.Mvvm
{
   public class MessageboxMessage : NotifyUserMessage
   {
      public MessageboxMessage(string message, ImageType image, Buttons buttons, string header = "") : base(message, header)
      {
         ImageType = ImageType;
         Buttons = buttons;
      }

      public ImageType ImageType { get; set; }
      public Buttons Buttons { get; set; }
   }

   public enum ImageType
   {
      None = 0,
      Information,
      Warnung,
      Fehler
   }

   public enum Buttons
   {
      Ok = 0,
      OkCancel,
      YesNo,
      YesNoCancel
   }
}
