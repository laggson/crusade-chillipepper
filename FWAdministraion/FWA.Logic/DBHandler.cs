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
        private readonly Configuration myConfiguration;
        private readonly ISessionFactory mySessionFactory;
        private readonly Control _con;

        public DBHandler(Control con)
        {
            //Initializing NHibernate's necessary Objects
            _con = con;
            myConfiguration = new Configuration();
            myConfiguration.Configure();
            myConfiguration.AddAssembly(typeof(Check).Assembly);
            new NHibernate.Tool.hbm2ddl.SchemaExport(myConfiguration).Execute(false, true, false);
            mySessionFactory = myConfiguration.BuildSessionFactory();
        }

        public void Insert(object obj, InsertionMode mode = InsertionMode.SaveOrUpdate, ISession session = null)
        {
            Execute(x =>
            {
                switch (mode)
                {
                    case InsertionMode.Save:
                        x.Save(obj); break;
                    case InsertionMode.Update:
                        x.Update(obj); break;
                    default:
                        x.SaveOrUpdate(obj); break;
                }
            }, session);
        }

        public void Execute(Action<ISession> action, ISession session = null)
        {
            ExecuteFunc(x =>
            {
                action.Invoke(x); return true;
            }, session);
        }

        public T ExecuteFunc<T>(Func<ISession, T> func, ISession session = null)
        {
            ITransaction transaction = null;
            bool newSession = false;
            try
            {
                if (session == null)
                {
                    mySessionFactory.OpenSession();
                    newSession = true;
                }

                transaction = session.BeginTransaction();
                var result = func.Invoke(session);
                transaction.Commit();
                return result;
            }
            catch
            {
                if (transaction != null)
                    transaction.Rollback();
                throw;
            }
            finally
            {
                if (newSession && session != null)
                    session.Close();
            }
        }

        #region Device-Transmission

        public void GetAllDeviceData()
        {
            this.GetTLFData();
            this.GetLFData();
            this.GetMTFData();
            this.GetHallData();
        }

        public IList<Device> GetTLFData()
        {
            var result = ExecuteFunc(session =>
            {
                ICriteria criteria = session.CreateCriteria<Device>()
                    .Add(Restrictions.Like("InvNumber", "__TF%"));

                return criteria.List<Device>();
            });

            _con.TFData = result;
            return result;
        }

        public IList<Device> GetLFData()
        {
            var result = ExecuteFunc(session =>
            {
                ICriteria criteria = session.CreateCriteria<Device>()
                    .Add(Restrictions.Like("InvNumber", "__LF%"));

                return criteria.List<Device>();
            });

            _con.LFData = result;
            return result;
        }

        public IList<Device> GetMTFData()
        {
            var result = ExecuteFunc(session =>
            {
                ICriteria criteria = session.CreateCriteria<Device>()
                    .Add(Restrictions.Like("InvNumber", "__MF%"));

                return criteria.List<Device>();
            });

            _con.MFData = result;
            return result;
        }

        public IList<Device> GetHallData()
        {
            var result = ExecuteFunc(session =>
            {
                ICriteria criteria = session.CreateCriteria<Device>()
                    .Add(Restrictions.Eq("InvNumber", string.Empty));

                return criteria.List<Device>();
            });


            _con.HallData = result;
            return result;
        }

        /// <summary>
        /// The parameter device is pushed to the database... Nobody saw that coming o_O
        /// </summary>
        /// <param name="device">The device to push</param>
        public void PushOrUpdateDevice(Device device)
        {
            //Not much to say here. Device is pushed to DB
            Execute(session => session.SaveOrUpdate(device));
        }

        /// <summary>
        /// Basically does what the name promises. Goes through the generic list and pushes every Device in it
        /// </summary>
        /// <param name="list">What would you guess this is for? :D</param>
        public void PushListOfDevices(List<Device> list)
        {
            foreach (Device d in list)
            {
                this.PushOrUpdateDevice(d);
            }
        }

        #endregion

        #region Check-Transmission

        public void PushOrUpdateCheck(Check check)
        {
            Execute(session => session.SaveOrUpdate(check));
        }

        public void PushListOfChecks(List<Check> list)
        {
            foreach (Check c in list)
            {
                this.PushOrUpdateCheck(c);
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

            var user = new User
            {
                Name = name,
                EMail = email,
                Hash = pwHash,
                Salt = salt
            };

            Insert(user, InsertionMode.Save);

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
            //All items in the user table matching the criteria go here
            var list = ExecuteFunc(session => 
            {
                var criteria = session.CreateCriteria<User>()
                                .Add(Restrictions.Eq(name.Contains("@") ? "EMail" : "Name", name));
                return criteria.List<User>();
            });

            //if there was one user matching the details (which should be) the data is stored for next steps
            var user = list.Single();


            //Entered password is again put into Byte[] to be encrypted with the same salt again
            var pwBytes = Encoding.UTF8.GetBytes(password.ToCharArray());
            string localHash = Crypter.Blowfish.Crypt(pwBytes, user.Salt);

            //If both hashes are the same, we can be sure that the entered password is correct
            if (localHash.Equals(user.Hash))
            {
                _con.ConnectedUser = user;
                return true;
            }
            return false;
        }

        #endregion

        /// <summary>
        /// Closes all connections and disposes all inner objects
        /// </summary>
        public void Dispose()
        {
            mySessionFactory.Dispose();
        }
    }

    public enum InsertionMode
    {
        Save, Update, SaveOrUpdate
    }
}
