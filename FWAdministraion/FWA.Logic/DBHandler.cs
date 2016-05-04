using System.Collections.Generic;
using NHibernate.Criterion;
using FWA.Logic.Storage;
using NHibernate.Cfg;
using System.Linq;
using System.Text;
using CryptSharp;
using NHibernate;
using System;

namespace FWA.Logic
{
    /// <summary>
    /// Handles everything a database connection is used for
    /// Controls user transmitting as well as device transmitting
    /// </summary>
    public class DBHandler : IDisposable
    {
        private Configuration myConfiguration;
        private ISessionFactory mySessionFactory;
        private ISession mySession;
        private Control _con;

        public DBHandler(Control con)
        {
            //Initializing NHibernate's necessary Objects
            _con = con;
            myConfiguration = new Configuration();
            myConfiguration.Configure();
            mySessionFactory = myConfiguration.BuildSessionFactory();
            mySession = mySessionFactory.OpenSession();
        }

        #region Device-Transmission

        public IList<Device> GetTLFData()
        {
            IList<Device> result;

            using (mySession.BeginTransaction())
            {
                ICriteria criteria = mySession.CreateCriteria<Device>()
                    .Add(Restrictions.Like("InvNumber", "__TF%"));

                //criteria.SetProjection(Projections.ProjectionList()
                //                .Add(Projections.Sum("Name")));

                result = criteria.List<Device>();

                foreach(Device d in result)
                {
                    // TODO: Group items
                    // Probably need to save the whole collection somewhere else,
                    // to make shure the items are not lost
                }

            }
            return result;
        }

        public IList<Device> GetLFData()
        {
            IList<Device> result;

            using (mySession.BeginTransaction())
            {
                ICriteria criteria = mySession.CreateCriteria<Device>()
                    .Add(Restrictions.Like("InvNumber", "__LF%"));

                result = criteria.List<Device>();

            }
            return result;
        }
        public IList<Device> GetMTFData()
        {
            IList<Device> result;

            using (mySession.BeginTransaction())
            {
                ICriteria criteria = mySession.CreateCriteria<Device>()
                    .Add(Restrictions.Like("InvNumber", "__MF%"));

                result = criteria.List<Device>();

            }
            return result;
        }

        public IList<Device> GetHallData()
        {
            IList<Device> result;

            using (mySession.BeginTransaction())
            {
                ICriteria criteria = mySession.CreateCriteria<Device>()
                    .Add(Restrictions.Eq("InvNumber", string.Empty));

                result = criteria.List<Device>();

            }
            return result;
        }

        /// <summary>
        /// The parameter device is pushed to the database... Nobody saw that coming o_O
        /// </summary>
        /// <param name="device">The device to push</param>
        public void PushDevice(Device device)
        {
            //Not much to say here. Device is pushed to DB
            using (mySession.BeginTransaction())
            {
                mySession.Save(device);
                mySession.Transaction.Commit();
            }
        }

        /// <summary>
        /// Basically does what the name promises. Goes through the generic list and pushes every Device in it
        /// </summary>
        /// <param name="list">What would you guess this is for? :D</param>
        public void PushListOfDevices(List<Device> list)
        {
            foreach(Device d in list)
            {
                this.PushDevice(d);
            }
        }

        #endregion

        #region User-Checking

        /// <summary>
        /// Uses the data entered in the parameters and saves them as a new user to the database
        /// together with a randomly generated salt, after encrypting the password with the salt
        /// </summary>
        /// <param name="name">The username that is to be stored in the DB</param>
        /// <param name="email">The EMail-address that is to be stored in the DB</param>
        /// <param name="password">The password that is to be saved after encrypting</param>
        /// <returns></returns>
        public User SerializeUser(string name, string email, string password)
        {
            //Saving password as Byte[], used for Blowfish's encryption.
            var pwBytes = Encoding.UTF8.GetBytes(password.ToCharArray());

            //Cryptsharp generates a random Salt and hashes the password
            string salt = Crypter.Blowfish.GenerateSalt();
            string pwHash = Crypter.Blowfish.Crypt(pwBytes, salt);

            User user;

            using (mySession.BeginTransaction())
            {
                //Creating a User object with the entered Data and the encryption
                user = new User
                {
                    Name = name,
                    EMail = email,
                    Hash = pwHash,
                    Salt = salt
                };

                //NHibernate pushes the object to my Database, at the end of using-clause all resources are recycled
                mySession.Save(user);
                mySession.Transaction.Commit();
            }

            return user;
        }

        /// <summary>
        /// Uses the entered name or email-address and password and compares them with the database,
        /// to see if the entered is correct.
        /// </summary>
        /// <param name="name">The name or EMail-Address to check for</param>
        /// <param name="password">The password</param>
        /// <returns></returns>
        public bool UserDataCorrect(string name, string password)
        {
            User user;

            using (mySession.BeginTransaction())
            {
                string searching = string.Empty;

                //Is the entered name an email address?
                if (name.Contains("@"))
                    searching = "EMail";
                else searching = "Name";

                //Declaring a Criteria the database is to be searched for, either mail or name
                ICriteria criteria = mySession.CreateCriteria<User>()
                    .Add(Restrictions.Eq(searching, name));

                //All items in the user table matching the criteria go here
                IList<User> list = criteria.List<User>();

                //if there was one user matching the details (which should be) the data is stored for next steps
                user = list.Single();
               
                mySession.Transaction.Commit();
            }

            //Entered password is again put into Byte[] to be encrypted with the same salt again
            var pwBytes = Encoding.UTF8.GetBytes(password.ToCharArray());
            string localHash = Crypter.Blowfish.Crypt(pwBytes, user.Salt);

            //If both hashes are the same, we can be shure that the entered password is correct
            if (localHash.Equals(user.Hash))
            {
                _con.ConnectedUser = user;
                return true;
            }
            else return false;
        }

        #endregion
        /// <summary>
        /// Closes all connections and disposes all inner objects
        /// </summary>
        public void Dispose()
        {
            mySession.Disconnect();
            mySession.Dispose();
            mySessionFactory.Dispose();
        }
    }
}
