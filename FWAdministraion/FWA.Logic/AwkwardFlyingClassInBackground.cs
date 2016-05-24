namespace FWA.Logic
{
    /// <summary>
    /// Verfügt momentan über keinen Nutzen, außer der Ausgabe der Assembly Version als string. Ist statisch.
    /// </summary>
    public static class AwkwardFlyingClassInBackground
    {
        /// <summary>
        /// Gibt die Assembly Version zurück, ausgelesen aus der AssemblyInfo.cs Datei. Formatiert als string ohne die Revisionsnummer
        /// </summary>
        public static string Version
        {
            get
            {
                System.Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                return ver.Major + "." + ver.Minor + "." + ver.Build;
            }
        }
    }
}
