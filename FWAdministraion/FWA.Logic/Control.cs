using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FWA.Logic
{
    public class Control
    {
        private IList<Device> _tf;
        private IList<Device> _lf;
        private IList<Device> _mf;
        private IList<Device> _hall;
        public event DataListChanged TFDataChanged;
        public event DataListChanged LFDataChanged;
        public event DataListChanged MFDataChanged;
        public event DataListChanged HallDataChanged;

        public Control()
        {

        }

        public IList<Device> DataListByName(string name)
        {
            switch (name)
            {
                case "TLF":
                    return this.TFData;
                case "LF":
                    return this.LFData;
                case "MTF":
                    return this.MFData;
                case "Halle":
                    return this.HallData;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Returns a new list with all the devices from the source list which have the exact same name
        /// </summary>
        /// <param name="name">The name to search for</param>
        /// <param name="source">The IList containing the vehicle data</param>
        /// <returns></returns>
        public List<Device> DevicesByName(string name, IList<Device> source)
        {
            List<Device> list = new List<Device>();

            foreach(Device d in source)
            {
                if (d.Name.Equals(name))
                    list.Add(d);
            }

            return list;
        }

        /// <summary>
        /// Returns a new List, which contains one and only one Item of each name
        /// </summary>
        /// <param name="source">The list of items for the specific location</param>
        /// <returns></returns>
        public List<Device> TrimList(IList<Device> source)
        {
            List<Device> list = new List<Device>();

            foreach(Device d in source)
            {
                //Check if any of the list items already has that name
                bool itemFound = list.Any(item => item.Name.Equals(d.Name));

                //If there's no item in the local list with the current name, insert it
                if (!itemFound)
                    list.Add(d);
            }

            return list;
        }

        #region Properties

        /// <summary>
        /// Returns a new instance of the DBHandler. For more see class documentation
        /// </summary>
        public DBHandler DBHandler
        {
            get
            {
                return new DBHandler(this);
            }
        }

        public User ConnectedUser
        {
            get; set;
        }

        public IList<Device> TFData
        {
            get
            {
                return _tf;
            }

            set
            {
                _tf = value;
                this.TFDataChanged(value, new EventArgs());
            }
        }

        public IList<Device> LFData
        {
            get
            {
                return _lf;
            }

            set
            {
                _lf = value;
                this.LFDataChanged(value, new EventArgs());
            }
        }

        public IList<Device> MFData
        {
            get
            {
                return _mf;
            }

            set
            {
                _mf = value;
                this.MFDataChanged(value, new EventArgs());
            }
        }

        public IList<Device> HallData
        {
            get
            {
                return _hall;
            }

            set
            {
                _hall = value;
                this.HallDataChanged(value, new EventArgs());
            }
        }

        #endregion

        /// <summary>
        /// Reads the current assembly version from the AssemblyInfo.cs and returns it merged to one string
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            System.Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return ver.Major + "." + ver.Minor + "." + ver.Build;
        }
    }

    public delegate void DataListChanged(object sender, EventArgs e);
}
