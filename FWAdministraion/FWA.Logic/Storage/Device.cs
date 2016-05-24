using System.ComponentModel;

namespace FWA.Logic.Storage
{
    /// <summary>
    /// Speichert die Informationen eines bestimmten Gegenstandes
    /// </summary>
    public class Device : IStorageItem
    {
        //private int[] _monthsToCheck;

        /// <summary>
        /// Gibt den Namen des aktuellen Gegenstandes zurück
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }


        /// <summary>
        /// Der Primärschlüssel. Vom Benutzer nicht benötigt
        /// </summary>
        [DisplayName("ID")]
        public virtual int ID
        {
            get; set;
        }

        //public int[] MonthsToCheck
        //{
        //    get {
        //        return _monthsToCheck;
        //    }

        //    set
        //    {
        //        if (value.Length < this?.AnnualChecks)
        //        {
        //            _monthsToCheck = value;
        //        }
        //        else Console.WriteLine("Angehägtes Array ist zu lang für die angegebene Zahl an jährlichen Prüfungen. Lol");
        //    }
        //}

        /// <summary>
        /// Der Name des Gegenstandes
        /// </summary>
        [DisplayName("Name")]
        public virtual string Name
        {
            get; set;
        }

        /// <summary>
        /// Die Inventar Nummer des Gegenstandes. Einmalig
        /// </summary>
        [DisplayName("Inventar-Nr.")]
        public virtual string InvNumber
        {
            get; set;
        }

        /// <summary>
        /// Liefert true, wenn für den Gegenstand eine vorgefertigte Prüfkarte angelegt werden muss
        /// </summary>
        [DisplayName("Prüfkarte")]
        public virtual bool NeedsCheckcard
        {
            get; set;
        }

        /// <summary>
        /// Die Anzahl der Prüfungen, die im Jahr für den Gegenstand durchgeführt werden müssen
        /// </summary>
        [DisplayName("Jährl. Prüfungen")]
        public virtual short AnnualChecks
        {
            get; set;
        }

        /// <summary>
        /// Ein Hinweis für den Anwender, auf was der Gegenstand überprüft werden muss
        /// </summary>
        [DisplayName("Durchzuf. Prüfung")]
        public virtual string KindOfCheck
        {
            get; set;
        }

        /// <summary>
        /// Eine Bemerkung zu dem Gegenstand. Resultierend aus Mängeln
        /// </summary>
        [DisplayName("Bemerkung")]
        public virtual string Comment
        {
            get; set;
        }

        /// <summary>
        /// Gibt den Aufbewahrungsort des Gegenstandes zurück, resultierend aus der Inventar Nummer
        /// </summary>
        /// <returns>Der Aufbewahrungsort des Gegenstandes</returns>
        protected virtual string GetLocation()
        {
            return InvNumber?.Split(' ')[1];
        }
    }
}
