using FWA.Core.Helpers;
using FWA.Core.Models;
using FWA.Core.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FWA.Core.ViewModels
{
   public class PruefungViewModel : ObservableObject
   {
      #region

      private ObservableCollection<Pruefung> pruefungen;
      public ObservableCollection<Pruefung> Pruefungen
      {
         get { return pruefungen; }
         set
         {
            pruefungen = value;
            NotifyPropertyChanged(nameof(Pruefungen));
         }
      }

      #endregion

      /// <summary>
      /// Ist zum Initialisieren des VM da und berechnet die benötigten Informationen aus dem angegebenen <see cref="Gegenstand"/>.
      /// </summary>
      /// <param name="gegenstand">Der Gegenstand, dessen Geschwistereinträge gezeigt werden sollen.</param>
      public void Init(Gegenstand gegenstand)
      {
         if (gegenstand == null)
            return;

         string invNummerLike;
         if (string.IsNullOrEmpty(gegenstand.InvNummer.Trim()))
         {
            invNummerLike = string.Empty;
         }
         else
         {
            invNummerLike = "__" + gegenstand.InvNummer.Substring(2, 2) + "%";
         }

         CreatePruefungen(invNummerLike, gegenstand.Bezeichnung);
      }

      public void Fertig()
      {
         var test = Pruefungen;
      }

      /// <summary>
      /// Lädt die zutreffenden Einträge aus der Datenbank und generiert daraus die Vorlage für die Prüfungen.
      /// </summary>
      /// <param name="invNummerLike"></param>
      /// <param name="bezeichnung"></param>
      private void CreatePruefungen(string invNummerLike, string bezeichnung)
      {
         // Prüfungen mit selbem Namen aus DB holen und anzeigen.
         var zutreffendeGegenstaende = DBAuthentication.Instance.GetDevicesByInvNumberType(invNummerLike, bezeichnung);
         if (!zutreffendeGegenstaende.All(item => item.GetLocation() == zutreffendeGegenstaende.First().GetLocation()))
            throw new ArgumentException("Items aus verschiedenen Fahrzeugen. Was da los...");

         // Prüfungen generieren
         var pruefungen = new List<Pruefung>();

         foreach (var gegenstand in zutreffendeGegenstaende)
         {
            pruefungen.Add(new Pruefung
            {
               Gegenstand = gegenstand,
               Datum = DateTime.Today,
               Tester = DBAuthentication.Instance.CurrentUser,
               Zustand = Zustand.Ok
            });
         }

         Pruefungen = new ObservableCollection<Pruefung>(pruefungen);
      }
   }
}
