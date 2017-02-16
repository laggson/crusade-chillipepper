using System;

namespace FWA2.Core.Models
{
   public class Pruefung
   {
      int id;
      public virtual int Id
      {
         get { return id; }
         set
         {
            id = value;
         }
      }

      Gegenstand gegenstand;
      public virtual Gegenstand Gegenstand
      {
         get { return gegenstand; }
         set
         {
            gegenstand = value;
         }
      }

      DateTime datum;
      public virtual DateTime Datum
      {
         get { return datum; }
         set
         {
            datum = value;
         }
      }
      
      User tester;
      public virtual User Tester
      {
         get { return tester; }
         set
         {
            tester = value;
         }
      }

      Zustand zustand;
      public virtual Zustand Zustand
      {
         get { return zustand; }
         set
         {
            zustand = value;
         }
      }

      string mangel;
      public virtual string Mangel
      {
         get { return mangel; }
         set
         {
            mangel = value;
         }
      }

      string kommentar;
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
