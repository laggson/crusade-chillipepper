namespace FWA.Core.Models
{
   public enum Zustand
   {
      // vl 0 - nicht benötigt dazu
      NochNichtGeprueft = 1,
      Ok,
      MangelGefunden,
      Repariert
   }

   public enum AccountType
   {
      Spectator = 0,
      User,
      Master
   }

   public enum Dialog
   {
      LoginWindow = 0,
      AboutWindow,
      PruefungWindow
   }

   public enum DialogStatus
   {
      Geoeffnet = 0,
      Fertig
   }
}
