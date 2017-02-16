namespace FWA2.Core.Models
{
   public class User
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
      public virtual string Name
      {
         get { return name; }
         set
         {
            name = value;
         }
      }

      string eMail;
      public virtual string EMail
      {
         get { return eMail; }
         set
         {
            eMail = value;
         }
      }

      string hash;
      public virtual string Hash
      {
         get { return hash; }
         set
         {
            hash = value;
         }
      }

      string salt;
      public virtual string Salt
      {
         get { return salt; }
         set
         {
            salt = value;
         }
      }

      AccountType accountType;
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
