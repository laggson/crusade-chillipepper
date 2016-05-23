namespace FWA.Logic
{
    public class AwkwardFlyingClassInBackground
    {
        /// <summary>
        /// Reads the current assembly version from the AssemblyInfo.cs and returns it merged to one string
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            System.Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return ver.Major + "." + ver.Minor + "." + ver.Build;
        }
    }
}
