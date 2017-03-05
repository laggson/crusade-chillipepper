using FWA.Core.Helpers;
using FWA.Core.Models;
using FWA.Core.Mvvm;
using System.Collections.Generic;

namespace FWA.Core.ViewModels
{
   public class PruefungViewModel : ObservableObject
   {
      #region
      
      private List<Pruefung> pruefungen;
      public List<Pruefung> Pruefungen
      {
         get { return pruefungen; }
         set { pruefungen = value; }
      }

      #endregion

      public void Init(Gegenstand gegenstand)
      {
         if (gegenstand == null)
            return;

         // Prüfungen mit selbem Namen aus DB holen und anzeigen.
      }
   }
}
