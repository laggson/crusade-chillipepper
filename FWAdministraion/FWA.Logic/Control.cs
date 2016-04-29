namespace FWA.Logic
{
    public class Control
    {

        public Control()
        {

        }

        public DBHandler DBHandler
        {
            get
            {
                return new DBHandler(this);
            }
        }

        public string GetVersion()
        {
            System.Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return ver.Major + "." + ver.Minor + "." + ver.Revision;
        }

        public User ConnectedUser
        {
            get; set;
        }
    }
}
