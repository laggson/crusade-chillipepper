using System;
using System.ComponentModel;

namespace FWA.Logic.Storage
{
    public class Check : IStorageItem
    {
        Device _device;

        public Check(Device d)
        {
            _device = d;
            this.Name = d.Name;
            this.InvNumber = d.InvNumber;
        }

        public int ID
        {
            get; set;
        }

        public string Name
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
        public string InvNumber
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

        public DateTime DateChecked
        {
            get; set;
        }

        [DisplayName("Geprüft am")]
        public string StringDate
        {
            get
            {
                return DateChecked.ToString("dd/MM/yyyy");
            }
        }
        
        [DisplayName("Prüfer")]
        public User WhoChecked
        {
            get; set;
        }

        [DisplayName("Zustand")]
        public CheckType CheckType
        {
            get; set;
        }

        [DisplayName("Mängel")]
        public string Lack
        {
            get; set;
        }

        [DisplayName("Bemerkung")]
        public string Comment
        {
            get; set;
        }
    }
}
