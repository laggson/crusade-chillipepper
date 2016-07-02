using System.Collections.Generic;
using System.ComponentModel;

namespace FWA.Logic.Storage
{
    /// <summary>
    /// Speichert die Informationen eines bestimmten Gegenstandes
    /// </summary>
    public class Device : IStorageItem
    {
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

        [DisplayName("J")]
        public virtual CheckType Januray
        {
            get; set;
        }

        [DisplayName("F")]
        public virtual CheckType February
        {
            get; set;
        }

        [DisplayName("M")]
        public virtual CheckType March
        {
            get; set;
        }

        [DisplayName("A")]
        public virtual CheckType April
        {
            get; set;
        }

        [DisplayName("M")]
        public virtual CheckType May
        {
            get; set;
        }

        [DisplayName("J")]
        public virtual CheckType June
        {
            get; set;
        }

        [DisplayName("J")]
        public virtual CheckType July
        {
            get; set;
        }

        [DisplayName("A")]
        public virtual CheckType August
        {
            get; set;
        }

        [DisplayName("S")]
        public virtual CheckType September
        {
            get; set;
        }

        [DisplayName("O")]
        public virtual CheckType October
        {
            get; set;
        }

        [DisplayName("N")]
        public virtual CheckType November
        {
            get; set;
        }

        [DisplayName("D")]
        public virtual CheckType December
        {
            get; set;
        }

        //public List<CheckType> MonthsToCheck()
        //{
        //    List<CheckType> list = new List<CheckType>();

        //    list.Add(Januray);
        //    list.Add(February);
        //    list.Add(March);
        //    list.Add(April);
        //    list.Add(May);
        //    list.Add(June);
        //    list.Add(July);
        //    list.Add(August);
        //    list.Add(September);
        //    list.Add(October);
        //    list.Add(November);
        //    list.Add(December);

        //    return list;
        //}

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
