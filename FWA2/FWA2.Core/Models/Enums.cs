namespace FWA2.Core.Models
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
}
