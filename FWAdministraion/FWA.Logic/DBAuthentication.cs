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
        readonly User _currentUser;

        public DBAuthentication(string username, byte[] password)
        {
            var userlist = DBAccess.GetByCriteria<User>(c => c.Add(Restrictions.Eq(username.Contains("@") ? "EMail" : "Name", username)));

            if (userlist.Count < 1)
                throw new AuthenticationException(username, "No such user");

            if (userlist.Count > 1)
                throw new AuthenticationException(username, "Multiple users found. Please fix your goddamn database");

            var user = userlist.Single();

            string newHash = Crypter.Blowfish.Crypt(password, user.Salt);
            if (!newHash.Equals(user.Hash))
            {
                throw new AuthenticationException(username, "Wrong password");
            }

            _currentUser = user;
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
            foreach(var obj in objects)
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
                throw new ArgumentException("Cannot insert Users");

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
                throw new ArgumentException(string.Format("A user with the username {0} already exists", username));
            }

            if (DBAccess.GetByCriteria<User>(c=> c.Add(Restrictions.Eq("EMail", email)), session).Count > 0)
            {
                throw new ArgumentException(string.Format("A user with the email {0} already exists", email));
            }
        }

        public bool HasRights(AccountType needed)
        {
            return (int)needed <= (int)_currentUser.AccountType;
        }

        public void AssertRights(AccountType needed, string action = "")
        {
            if (!HasRights(needed))
            {
                throw new InsufficientRightsException(_currentUser, needed, action);
            }
        }
    }
}
