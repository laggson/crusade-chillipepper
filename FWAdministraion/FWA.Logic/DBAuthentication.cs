using CryptSharp;
using FWA.Logic.Exceptions;
using FWA.Logic.Storage;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FWA.Logic
{
    public class DBAuthentication
    {
        public User CurrentUser { get; }

        public DBAuthentication(string username, byte[] password)
        {
            var userlist = DBAccess.GetByCriteria<User>(c => c.Add(Restrictions.Eq(username.Contains("@") ? "EMail" : "Name", username)));

            if (userlist.Count < 1)
                throw new AuthenticationException(username, "Der Nutzer wurde nicht gefunden.");

            if (userlist.Count > 1)
                throw new AuthenticationException(username, "Mehrere Nutzer des Namens gefunden. Dein Entwickler ist inkompetent.");

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

        public List<Device> GetDevicesByInvNumberType(string invNumberLike)
        {
            return DBAccess.GetByCriteria<Device>(c => c.Add(Restrictions.Like("InvNumber", invNumberLike)));
        }

        public void Insert(object obj)
        {
            AssertInsert(obj);
            DBAccess.Insert(obj);
        }

        public void InsertMultiple(IEnumerable objects)
        {
            foreach (var obj in objects)
            {
                AssertInsert(obj);
            }

            DBAccess.InsertMultiple(objects);
        }

        private void AssertInsert(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            if (obj is User)
                throw new ArgumentException("Kann keine Benutzer einfügen.");

            AssertRights(AccountType.User, "Insertion of " + obj.GetType().FullName);
        }

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

        public bool HasRights(AccountType needed)
        {
            return (int)needed <= (int)CurrentUser.AccountType;
        }

        public void AssertRights(AccountType needed, string action = "")
        {
            if (!HasRights(needed))
            {
                throw new InsufficientRightsException(CurrentUser, needed, action);
            }
        }
    }
}
