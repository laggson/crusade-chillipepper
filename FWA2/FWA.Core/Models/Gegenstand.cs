namespace FWA.Core.Models
{
   public class Gegenstand
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

      private Zeitraum zeitraumMap;
      /// <summary>
      /// Der Zeitraum, an denen der Gegenstand geprüft werden muss.
      /// </summary>
      public virtual Zeitraum Zeitraum
      {
         get { return zeitraumMap; }
         set { zeitraumMap = value; }
      }

      string bezeichnung;
      /// <summary>
      /// Der Name des Gegenstandes.
      /// </summary>
      public virtual string Bezeichnung
      {
         get { return bezeichnung; }
         set
         {
            bezeichnung = value;
         }
      }

      string invNummer;
      /// <summary>
      /// Die identische Inventar-Nummer des Gegenstandes.
      /// </summary>
      public virtual string InvNummer
      {
         get { return invNummer; }
         set
         {
            invNummer = value;
         }
      }

      bool brauchtPruefkarte;
      /// <summary>
      /// Gibt an, ob der Gegenstand eine Prüfkarte benötigt.
      /// </summary>
      public virtual bool BrauchtPruefkarte
      {
         get { return brauchtPruefkarte; }
         set
         {
            brauchtPruefkarte = value;
         }
      }

      string artDerPruefung;
      /// <summary>
      /// Gibt einen Hinweis für den Nutzer, was geprüft werden soll.
      /// </summary>
      public virtual string ArtDerPruefung
      {
         get { return artDerPruefung; }
         set
         {
            artDerPruefung = value;
         }
      }

      string kommentar;
      /// <summary>
      /// Ein möglicher Kommentar des Nutzers. Kann nach Rücksprache vielleicht raus.
      /// </summary>
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

      /// <summary>
      /// Gibt den dritten und vierten Character der <see cref="InvNummer"/> zurück, die den Ort des Gegenstandes bilden.
      /// </summary>
      /// <returns></returns>
      public virtual string GetLocation()
      {
         if (string.IsNullOrEmpty(invNummer))
            return string.Empty;

         return invNummer.Substring(2, 2);
      }
   }
}
