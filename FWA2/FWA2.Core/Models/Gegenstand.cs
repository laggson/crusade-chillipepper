namespace FWA2.Core.Models
{
   public class Gegenstand
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

      string name;
      public virtual string Bezeichnung
      {
         get { return name; }
         set
         {
            name = value;
         }
      }

      string invNummer;
      public virtual string InvNummer
      {
         get { return invNummer; }
         set
         {
            invNummer = value;
         }
      }

      bool brauchtPruefkarte;
      public virtual bool BrauchtPruefkarte
      {
         get { return brauchtPruefkarte; }
         set
         {
            brauchtPruefkarte = value;
         }
      }

      string artDerPruefung;
      public virtual string ArtDerPruefung
      {
         get { return artDerPruefung; }
         set
         {
            artDerPruefung = value;
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

      public override string ToString()
      {
         return Bezeichnung;
      }

      public virtual string GetLocation()
      {
         if (string.IsNullOrEmpty(invNummer))
            return string.Empty;

         return invNummer.Substring(2, 2);
      }
   }
}
