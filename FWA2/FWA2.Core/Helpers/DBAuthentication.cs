using CryptSharp;
using FWA2.Core.Exceptions;
using FWA2.Core.Models;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FWA2.Core.Helpers
{
   /// <summary>
   /// Die für den Anwender zugängliche Klasse zur Verwaltung der Datenbank
   /// </summary>
   public class DBAuthentication
   {
      /// <summary>
      /// Der Nutzer, der aktuell in die Datenbank eingeloggt ist.
      /// </summary>
      public User CurrentUser { get; }

      public static DBAuthentication Instance { get; private set; }

      /// <summary>
      /// Initialisiert eine neue Instanz der <see cref="DBAuthentication"/> mit den eingegebenen Benutzerdaten
      /// </summary>
      /// <param name="username">Der eingegebene Benutzername</param>
      /// <param name="password">Das eingegebene Passwort als Byte Array</param>
      private DBAuthentication(string username, byte[] password)
      {
         List<User> userlist = null;

         try
         {
            userlist = DBAccess.GetByCriteria<User>(c => c.Add(Restrictions.Eq(username.Contains("@") ? "EMail" : "Name", username)));
         }

         catch (Exception e)
         {
            throw e;
         }

         if (userlist.Count < 1)
            throw new AuthenticationException(username, "Der Nutzer wurde nicht gefunden.");

         if (userlist.Count > 1)
            throw new AuthenticationException(username, "Mehrere Nutzer des Namens gefunden." + Environment.NewLine + "Emfehlung des Hauses: Entwickler wechseln.");

         var user = userlist.Single();
         string newHash = Crypter.Blowfish.Crypt(password, user.Salt);

         //Clear password array
         for (int i = 0; i < password.Length; i++)
         {
            password[i] = byte.MinValue;
         }

         if (!newHash.Equals(user.Hash))
         {
            throw new AuthenticationException(username, "Falsches Passwort");
         }

         CurrentUser = user;
      }

      /// <summary>
      /// Erstellt ein Objekt für die Eigenschaft 'Instance', falls die Daten richtig sind, und löst sonst eine Ausnahme aus.
      /// </summary>
      /// <param name="user"></param>
      /// <param name="password"></param>
      public static void Create(string user, byte[] password)
      {
         Instance = new DBAuthentication(user, password);
      }

      /// <summary>
      /// Gibt eine Liste mit Devices zurück, deren Inventar Nummer die übergebene Zeichenfolge <paramref name="invNumberLike"/> enthalten
      /// </summary>
      /// <param name="invNumberLike">Die Zeichenfolge, die in der InvNumber der zurückzugebenen Devices enthalten sein soll</param>
      /// <returns></returns>
      public List<Gegenstand> GetDevicesByInvNumberType(string invNumberLike)
      {
         return DBAccess.GetByCriteria<Gegenstand>(c => c.Add(Restrictions.Like("InvNummer", invNumberLike)));
      }

      public List<Gegenstand> GetAlleGegenstaende()
      {
         return DBAccess.GetByCriteria<Gegenstand>(c => c.List());
      }

      /// <summary>
      /// Fügt das eingegebene Objekt in die Datenbank ein. Dabei wird automatisch entschieden, ob es angehängt oder aktualisiert wird
      /// </summary>
      /// <param name="obj"></param>
      public void Insert(object obj)
      {
         AssertInsert(obj);
         DBAccess.Insert(obj);
      }

      /// <summary>
      /// Fügt eine Aufzählung mehrerer Objekte in die Datenbank ein. Es wird automatisch entschieden, ob diese angehängt oder aktualisiert werden.
      /// </summary>
      /// <param name="objects">Die Aufzählung der anzuhängenden Objekte</param>
      public void InsertMultiple(IEnumerable objects)
      {
         foreach (var obj in objects)
         {
            AssertInsert(obj);
         }

         DBAccess.InsertMultiple(objects);
      }

      /// <summary>
      /// Prüft, ob das übergebene Objekt in die Datenbank eingefügt werden kann und löst eine Ausnahme aus, falls dies nicht der Fall ist.
      /// </summary>
      /// <exception cref="ArgumentException">Wird ausgelöst, falls versucht wird, einen User anzuhängen </exception>
      /// <exception cref="ArgumentNullException">Wird ausgelöst, falls das angegebene Objekt null ist</exception>
      /// <param name="obj">Das zu prüfende Objekt</param>
      private void AssertInsert(object obj)
      {
         if (obj == null)
            throw new ArgumentNullException();

         if (obj is User)
            throw new ArgumentException("Kann keine Benutzer einfügen.");

         AssertRights(AccountType.User, "Insertion of " + obj.GetType().FullName);
      }

      /// <summary>
      /// Speichert einen neuen Nutzer mit den angegebenen Daten in der Datenbank
      /// </summary>
      /// <param name="username">Der Name des Nutzers. Kann zur Anmeldung genutzt werden</param>
      /// <param name="email">Die E-Mail Adresse des Nutzers. Kann zur Anmeldung benutzt werden</param>
      /// <param name="password">Das Passwort des Nutzers. Wird mit Blowfish verschlüsselt gespeichert</param>
      public void CreateNewUser(string username, string email, byte[] password)
      {
         AssertRights(AccountType.Master, "User creation");

         var session = DBAccess.OpenSession();

         AssertUnique(username, email, session);

         //Cryptsharp generates a random Salt and hashes the password
         string salt = Crypter.Blowfish.GenerateSalt();
         string pwHash = Crypter.Blowfish.Crypt(password, salt);

         var user = new User
         {
            Name = username,
            EMail = email,
            Hash = pwHash,
            Salt = salt
         };

         DBAccess.Insert(user, InsertionMode.Save, session);
      }

      private void AssertUnique(string username, string email, ISession session)
      {
         if (DBAccess.GetByCriteria<User>(c => c.Add(Restrictions.Eq("Name", username)), session).Count > 0)
         {
            throw new ArgumentException(string.Format("Ein Benutzer mit dem Namen {0} existiert bereits.", username));
         }

         if (DBAccess.GetByCriteria<User>(c => c.Add(Restrictions.Eq("EMail", email)), session).Count > 0)
         {
            throw new ArgumentException(string.Format("Ein Benutzer mit der EMail-Adresse {0} existiert bereits.", email));
         }
      }

      /// <summary>
      /// Vergleicht das eingegebene Rechte-Level mit dem des aktuellen Nutzers und gibt einen bool zurück, ob die Aktion erlaubt ist
      /// </summary>
      /// <param name="needed">Das für die Operation benötigte Rechte-Level</param>
      /// <returns></returns>
      public bool HasRights(AccountType needed)
      {
         return (int)needed <= (int)CurrentUser.AccountType;
      }

      /// <summary>
      /// Versucht, die Berechtigung für den Nutzer festzulegen und löst eine Ausnahme aus, 
      /// falls das Rechte-Level des Nutzers zu gering für die Aktion ist.
      /// </summary>
      /// <exception cref="InsufficientRightsException">Wird ausgelöst, falls das Rechte-Level des Nutzers zu gering ist</exception>
      /// <param name="needed">Das für die Operation benötigte Rechte-Level</param>
      /// <param name="action">Die Aktion, die versucht wurde aufzurufen</param>
      public void AssertRights(AccountType needed, string action = "")
      {
         if (!HasRights(needed))
         {
            throw new InsufficientRightsException(CurrentUser, needed, action);
         }
      }

      /// <summary>
      /// Prüft, ob eine Verbindung zum Server hergestellt werden kann.
      /// </summary>
      /// <returns></returns>
      public bool TestServerConnection()
      {
         return true;

      }
   }
}
