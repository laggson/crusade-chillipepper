namespace FWA.Core.Mvvm
{
   /// <summary>
   /// Stellt ein Datenobjekt dar, das im MainWindow die eingegebene Nachricht als Popup-Fenster anzeigt.
   /// </summary>
   public class MessageboxMessage : NotifyUserMessage
   {
      /// <summary>
      /// Erstellt eine neue Instanz der <see cref="MessageboxMessage"/>-Klasse.
      /// </summary>
      /// <param name="message">Die Nachricht, die angezeigt werden soll.</param>
      /// <param name="image">Die Art des Icons, die im Dialog angezeigt werden soll.</param>
      /// <param name="buttons">Die Art der Buttons, die im Dialog angezeigt werden sollen.</param>
      /// <param name="header">Die Überschrift der angezeigten Nachricht.</param>
      public MessageboxMessage(string message, ImageType image, Buttons buttons, string header = "") : base(message, header)
      {
         ImageType = image;
         Buttons = buttons;
      }

      /// <summary>
      /// Die Art des Icons, die im Dialog angezeigt werden soll.
      /// </summary>
      public ImageType ImageType { get; set; }

      /// <summary>
      /// Die Art der Buttons, die im Dialog angezeigt werden sollen.
      /// </summary>
      public Buttons Buttons { get; set; }
   }

   /// <summary>
   /// Stellt ein Icon dar, das in einer Messagebox angezeigt wird.
   /// </summary>
   public enum ImageType
   {
      None = 0,
      Information,
      Warnung,
      Fehler
   }

   /// <summary>
   /// Stellt eine Menge von Buttons dar, die in einer Messagebox angezeigt werden.
   /// </summary>
   public enum Buttons
   {
      Ok = 0,
      OkCancel,
      YesNo,
      YesNoCancel
   }
}
