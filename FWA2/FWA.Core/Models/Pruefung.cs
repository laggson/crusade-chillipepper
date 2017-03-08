using System;

namespace FWA.Core.Models
{
   public class Pruefung
   {
      int id;
      /// <summary>
      /// Der Datenbank-Primärschlüssel des Objekts. Braucht man nicht zu verändern.
      /// </summary>
      public virtual int Id
      {
         get { return id; }
         set
         {
            id = value;
         }
      }

      Gegenstand gegenstand;
      /// <summary>
      /// Der Gegenstand, der geprüft wird. Wird für Fremdschlüssel benötigt.
      /// </summary>
      public virtual Gegenstand Gegenstand
      {
         get { return gegenstand; }
         set
         {
            gegenstand = value;
         }
      }


      DateTime datum;
      /// <summary>
      /// Das Datum, an dem geprüft wird. Nur monatsweise angezeigt.
      /// </summary>
      public virtual DateTime Datum
      {
         get { return datum; }
         set
         {
            datum = value;
         }
      }

      User tester;
      /// <summary>
      /// Der Nutzer, der den Test durchgeführt hat.
      /// </summary>
      public virtual User Tester
      {
         get { return tester; }
         set
         {
            tester = value;
         }
      }

      Zustand zustand;
      /// <summary>
      /// Der Zustand, der zum Zeitpunkt der Prüfung festgestellt wurde.
      /// </summary>
      public virtual Zustand Zustand
      {
         get { return zustand; }
         set
         {
            zustand = value;
         }
      }

      string mangel;
      /// <summary>
      /// Gibt einen Hinweis für weitere Nutzer, welcher Mangel gefunden wurde.
      /// </summary>
      public virtual string Mangel
      {
         get { return mangel; }
         set
         {
            mangel = value;
         }
      }

      string kommentar;
      /// <summary>
      /// Ein möglicher sonstiger Kommentar für weitere Nutzer.
      /// </summary>
      public virtual string Kommentar
      {
         get { return kommentar; }
         set
         {
            kommentar = value;
         }
      }
      
   }
}
