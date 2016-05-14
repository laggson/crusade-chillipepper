using System;
using System.ComponentModel;

namespace FWA.Logic.Storage
{
    public class Check
    {
        public virtual int ID
        {
            get; set;
        }

        public virtual Device Device
        {
            get; set;
        }

        public virtual User Tester
        {
            get; set;
        }

        public virtual DateTime DateChecked
        {
            get; set;
        }

        [DisplayName("Zustand")]
        public virtual CheckType CheckType
        {
            get; set;
        }

        [DisplayName("Mängel")]
        public virtual string Lack
        {
            get; set;
        }

        [DisplayName("Bemerkung")]
        public virtual string Comment
        {
            get; set;
        }

        [DisplayName("Geprüft am")]
        public virtual string DateCheckedString
        {
            get { return DateChecked.ToShortDateString(); }
        }

        [DisplayName("Inventar-Nr.")]
        public virtual string DeviceInvNumber
        {
            get { return Device?.InvNumber ?? string.Empty; }
        }

        [DisplayName("Prüfer")]
        public virtual string TesterName
        {
            get { return Tester?.Name ?? string.Empty; }
        }
    }

    public enum CheckType
    {
        NotNeeded,
        NotYetChecked,
        OK,
        LacksFound,
        Repaired
    }
}
