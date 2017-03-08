namespace FWA.Core.Models
{
   public class User
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
      

      string name;
      /// <summary>
      /// Name oder Kürzel des Nutzers.
      /// </summary>
      public virtual string Name
      {
         get { return name; }
         set
         {
            name = value;
         }
      }

      string eMail;
      /// <summary>
      /// Die E-Mail Adresse, falls wir das mal brauchen. Auch zum Anmelden da.
      /// </summary>
      public virtual string EMail
      {
         get { return eMail; }
         set
         {
            eMail = value;
         }
      }

      string hash;
      /// <summary>
      /// Der Passwort Hash, der in der DB gespeichert wird.
      /// </summary>
      public virtual string Hash
      {
         get { return hash; }
         set
         {
            hash = value;
         }
      }

      string salt;
      /// <summary>
      /// Das Salt, mit dem der Hash generiert wurde.
      /// </summary>
      public virtual string Salt
      {
         get { return salt; }
         set
         {
            salt = value;
         }
      }

      AccountType accountType;
      /// <summary>
      /// Gibt an, ob es sich um einen normalen User, Admin, etc. handelt.
      /// </summary>
      public virtual AccountType AccountType
      {
         get { return accountType; }
         set
         {
            accountType = value;
         }
      }

      public override string ToString()
      {
         return name;
      }
   }
}
