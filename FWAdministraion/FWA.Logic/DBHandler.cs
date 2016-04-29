using CryptSharp;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace FWA.Logic
{
    public class DBHandler : IDisposable
    {
        private Configuration myConfiguration;
        private ISessionFactory mySessionFactory;
        private ISession mySession;
        private Control _con;

        public DBHandler(Control con)
        {
            _con = con;
            myConfiguration = new Configuration();
            myConfiguration.Configure();
            mySessionFactory = myConfiguration.BuildSessionFactory();
            mySession = mySessionFactory.OpenSession();
        }

        public User SerializeUser(string name, string email, string password)
        {
            var pwBytes = Encoding.UTF8.GetBytes(password.ToCharArray());
            var mailBytes = Encoding.UTF8.GetBytes(email.ToCharArray());

            string salt = Crypter.Blowfish.GenerateSalt();
            string pwHash = Crypter.Blowfish.Crypt(pwBytes, salt);

            User user;

            using (mySession.BeginTransaction())
            {
                user = new User
                {
                    Name = name,
                    EMail = email,
                    Hash = pwHash,
                    Salt = salt
                };

                mySession.Save(user);
                mySession.Transaction.Commit();
            }

            return user;
        }

        public bool UserDataCorrect(string name, string password)
        {
            User user;

            using (mySession.BeginTransaction())
            {
                string searching = string.Empty;

                if (name.Contains("@"))
                    searching = "EMail";
                else searching = "Name";

                ICriteria criteria = mySession.CreateCriteria<User>()
                    .Add(Restrictions.Eq(searching, name));

                IList<User> list = criteria.List<User>();

                if (list.Count == 0)
                    return false;

                user = list.Single();
               
                mySession.Transaction.Commit();
            }
            var pwBytes = Encoding.UTF8.GetBytes(password.ToCharArray());
            string localHash = Crypter.Blowfish.Crypt(pwBytes, user.Salt);
            _con.ConnectedUser = user;
            return localHash.Equals(user.Hash);
        }

        public void Dispose()
        {
            mySession.Disconnect();
            mySession.Dispose();
            mySessionFactory.Dispose();
        }
    }
}
