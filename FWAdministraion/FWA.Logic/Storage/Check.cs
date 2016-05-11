using System;
using System.ComponentModel;
using System.Globalization;

namespace FWA.Logic.Storage
{
    public class Check : IStorageItem
    {
        Device _device;

        public Check() { }

        public Check(Device d)
        {
            _device = d;

#pragma warning disable RECS0021 // Warns about calls to virtual member functions occuring in the constructor
            Name = d.Name;
            InvNumber = d.InvNumber;
#pragma warning restore RECS0021 // Warns about calls to virtual member functions occuring in the constructor
        }

        public virtual int ID
        {
            get; set;
        }

        public virtual string Name
        {
            get
            {
                return _device.Name;
            }

            set
            {
                _device.Name = value;
            }
        }

        [DisplayName("Inventar-Nr.")]
        public virtual string InvNumber
        {
            get
            {
                return _device.InvNumber;
            }
            set
            {
                _device.InvNumber = value;
            }
        }

        public virtual DateTime DateChecked
        {
            get; set;
        }

        [DisplayName("Geprüft am")]
        public virtual string StringDate
        {
            get { return DateChecked.ToString("dd/MM/yyyy"); }
            set { DateChecked = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.CurrentCulture); }
        }

        public virtual int WhoCheckedID
        {
            get { return WhoChecked.ID; }
            set { WhoChecked = DBAccess.GetById<User>(value); }
        }

        [DisplayName("Prüfer")]
        public virtual User WhoChecked
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
