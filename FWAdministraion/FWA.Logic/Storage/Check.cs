using System;
using System.ComponentModel;

namespace FWA.Logic.Storage
{
    /// <summary>
    /// Speichert die Überprüfung eines einzelnen Devices und dessen Resultat für einen bestimmten Monat
    /// </summary>
    public class Check
    {
        /// <summary>
        /// Der Primärschlüssel. Vom Benutzer nicht benötigt.
        /// </summary>
        public virtual int ID
        {
            get; set;
        }

        /// <summary>
        /// Das Device, das überprüft wird
        /// </summary>
        [DisplayName("Gegenstand")]
        public virtual Device Device
        {
            get; set;
        }

        /// <summary>
        /// Die InvNumber des Device, das überprüft wird
        /// </summary>
        [DisplayName("Inventar-Nr.")]
        public virtual string DeviceInvNumber
        {
            get { return Device?.InvNumber ?? string.Empty; }
        }

        /// <summary>
        /// Das Datum der Überprüfung. Wird automatisch gesetzt
        /// </summary>
        public virtual DateTime DateChecked
        {
            get; set;
        }

        /// <summary>
        /// Das Datum der Überprüfung, konvertiert in einen kurzen Datums-String
        /// </summary>
        [DisplayName("Geprüft am")]
        public virtual string DateCheckedString
        {
            get { return DateChecked.ToShortDateString(); }
        }

        /// <summary>
        /// Der Benutzer, der die Überprüfung durchgeführt hat. Wird automatisch gesetzt
        /// </summary>
        [DisplayName("Prüfer")]
        public virtual User Tester
        {
            get; set;
        }

        /// <summary>
        /// Der Zustand des Gegenstands
        /// </summary>
        [DisplayName("Zustand")]
        public virtual CheckType CheckType
        {
            get; set;
        }

        /// <summary>
        /// Die gefundenen Mängel am entsprechenden Gegenstand. Leer, falls dieser in Ordnung ist
        /// </summary>
        [DisplayName("Mängel")]
        public virtual string Lack
        {
            get; set;
        }

        /// <summary>
        /// Eine Anmerkung zum Gefundenen Mangel am Gegenstand. Leer, falls dieser in Ordnung ist
        /// </summary>
        [DisplayName("Bemerkung")]
        public virtual string Comment
        {
            get; set;
        }
    }

    /// <summary>
    ///  Gibt an, ob Mängel vorhanden sind oder repariert wurden.
    /// </summary>
    public enum CheckType
    {
        /// <summary>
        /// Eine Überprüfung ist für diesen Monat nicht benötigt
        /// </summary>
        NotNeeded,

        /// <summary>
        /// Die Überprüfung wurde noch nicht durchgeführt
        /// </summary>
        NotYetChecked,

        /// <summary>
        /// Der Gegenstand ist in Ordnung
        /// </summary>
        OK,

        /// <summary>
        /// Es wurden Mängel am Gegenstand gefunden
        /// </summary>
        LacksFound,

        /// <summary>
        /// Die Mängel der vorherigen Überprüfung wurden behoben
        /// </summary>
        Repaired
    }
}
