using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWA.Logic.Storage
{
    public class YearCheck : IStorageItem
    {
        private Device device;
        private CheckType[] type;

        public YearCheck(Device d)
        {
            device = d;
            type = new CheckType[12];
        }

        public int ID
        {
            get; set;
        }

        public string Name
        {
            get
            {
                return device.Name;
            }

            set
            {
                device.Name = value;
            }
        }

        public string InvNumber
        {
            get
            {
                return device.InvNumber;
            }
        }

        public CheckType Jan
        {
            get
            {
                return type[0];
            }
        }

        public CheckType Feb
        {
            get
            {
                return type[1];
            }
        }

        public CheckType Mar
        {
            get
            {
                return type[2];
            }
        }

        public CheckType Apr
        {
            get
            {
                return type[3];
            }
        }

        public CheckType May
        {
            get
            {
                return type[4];
            }
        }

        public CheckType Jun
        {
            get
            {
                return type[5];
            }
        }

        public CheckType Jul
        {
            get
            {
                return type[6];
            }
        }

        public CheckType Aug
        {
            get
            {
                return type[7];
            }
        }

        public CheckType Sep
        {
            get
            {
                return type[8];
            }
        }

        public CheckType Oct
        {
            get
            {
                return type[9];
            }
        }

        public CheckType Nov
        {
            get
            {
                return type[10];
            }
        }

        public CheckType Dec
        {
            get
            {
                return type[11];
            }
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
