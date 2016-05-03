using FWA.Logic.Storage;

namespace FWA.Logic
{
    public class Control
    {
        //private ExcelImporter exc;

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
            return ver.Major + "." + ver.Minor + "." + ver.Build + "." + ver.Revision;
        }

        public User ConnectedUser
        {
            get; set;
        }

        public void StartExcelImport()
        {
            this.DBHandler.PushListOfDevices(new ExcelImporter().ImportLocalCSV());
        }
    }
}
