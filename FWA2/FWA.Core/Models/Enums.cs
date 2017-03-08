using System.ComponentModel;

namespace FWA.Core.Models
{
   public enum Zustand
   {
      // vl 0 - nicht benötigt dazu
      [Description("Noch nicht geprüft")]
      NochNichtGeprueft = 1,

      [Description("Ok")]
      Ok,

      [Description("Defekt")]
      Defekt,

      [Description("Repariert")]
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
      PruefungWindow,
      BenutzerWindow
   }

   public enum DialogStatus
   {
      Geoeffnet = 0,
      Fertig
   }
}
