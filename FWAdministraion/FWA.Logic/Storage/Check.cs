using System;

namespace FWA.Logic.Storage
{
    public class Check : IStorageItem
    {
        Device _device;

        public Check(Device d)
        {
            _device = d;
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

        public string StringDate
        {
            get
            {
                return DateChecked.ToString("dd/MM/yyyy");
            }
        }

        public User WhoChecked
        {
            get; set;
        }

        public string Lack
        {
            get; set;
        }

        public string Comment
        {
            get; set;
        }
    }
}
