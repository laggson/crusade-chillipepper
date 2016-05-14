using System.Collections.Generic;
using NHibernate.Criterion;
using FWA.Logic.Storage;
using System.Linq;
using System.Text;
using CryptSharp;

namespace FWA.Logic
{
    /// <summary>
    /// Handles everything a database connection is used for
    /// Controls user transmitting as well as device transmitting
    /// </summary>
    public class DBHandler
    {
        private readonly Control _con;

        public DBHandler(Control con)
        {
            //Initializing NHibernate's necessary Objects
            _con = con;
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
            var result = DBAccess.GetByCriteria<Device>(c => c.Add(Restrictions.Like("InvNumber", "__TF%")));

            _con.TFData = result;
            return result;
        }

        public IList<Device> GetLFData()
        {
            var result = DBAccess.GetByCriteria<Device>(c => c.Add(Restrictions.Like("InvNumber", "__LF%")));

            _con.LFData = result;
            return result;
        }

        public IList<Device> GetMTFData()
        {
            var result = DBAccess.GetByCriteria<Device>(c => c.Add(Restrictions.Like("InvNumber", "__MF%")));

            _con.MFData = result;
            return result;
        }

        public IList<Device> GetHallData()
        {
            var result = DBAccess.GetByCriteria<Device>(c => c.Add(Restrictions.Like("InvNumber", string.Empty)));

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
            DBAccess.Insert(device);
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
            DBAccess.Insert(check);
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

            DBAccess.Insert(user, InsertionMode.Save);

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
            var list = DBAccess.GetByCriteria<User>(c => c.Add(Restrictions.Eq(name.Contains("@") ? "EMail" : "Name", name)));

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
    }
}
