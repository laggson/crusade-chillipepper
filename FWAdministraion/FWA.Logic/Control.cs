using FWA.Logic.Storage;

namespace FWA.Logic
{
    public class Control
    {

        public Control()
        {
        }

        /// <summary>
        /// Returns a new instance of the DBHandler. For more see class documentation
        /// </summary>
        public DBHandler DBHandler
        {
            get
            {
                return new DBHandler(this);
            }
        }

        /// <summary>
        /// Reads the current version of the tool from the AssemblyInfo.cs and returns it merged to one string
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            System.Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return ver.Major + "." + ver.Minor + "." + ver.Build; // + "." + ver.Revision;
        }

        public User ConnectedUser
        {
            get;set;
        }
    }
}
