namespace FWA.Logic.Storage
{
    /// <summary>
    /// Speichert die Informationen eines bestimmten Nutzers
    /// </summary>
    public class User : IStorageItem
    {
        /// <summary>
        /// Der Primärschlüssel. Vom Benutzer nicht benötigt.
        /// </summary>
        public virtual int ID
        {
            get; set;
        }
        
        /// <summary>
        /// Der Name des Benutzers
        /// </summary>
        public virtual string Name
        {
            get; set;
        }

        /// <summary>
        /// Die E-Mail Adresse des Benutzers
        /// </summary>
        public virtual string EMail
        {
            get; set;
        }

        /// <summary>
        /// Das gehashte Passwort des Nutzers. Aus Sicherheitsgründen nicht direkt gespeichert
        /// </summary>
        public virtual string Hash
        {            
            get; set;
        }

        /// <summary>
        /// Das zufällig generierte Salz, mit dem der Passwort-Hash generiert wird. Sorgt für erhöhte Sicherheit
        /// </summary>
        public virtual string Salt
        {
            get; set;
        }

        /// <summary>
        /// Die Art des Accounts, die den Rechte-Level festlegt
        /// </summary>
        public virtual AccountType AccountType
        {
            get; set;
        }

        /// <summary>
        /// Gibt den Namen des Nutzers zurück
        /// </summary>
        /// <returns>Der Name des Benutzers</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }

    /// <summary>
    /// Die Level des Benutzers. Legt den Rechte-Level fest
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Uneingeschränkter Zugriff auf alle Funktionen
        /// </summary>
        Master,

        /// <summary>
        /// Ein gewöhnlicher Benutzer, der die gängigen Operationen durchführen kann
        /// </summary>
        User,

        /// <summary>
        /// Ein Zuschauer-Konto, das die Daten einsehen, aber nicht verändern kann
        /// </summary>
        Spectator
    }
}
