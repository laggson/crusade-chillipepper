using FWA.Logic.Storage;
using System;
using System.Collections.Generic;
using System.Data;

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

        //public Device DataRowToDevice(DataRow data)
        //{
        //    Device d;
        //    try
        //    {

        //        d = new Device()
        //        {
        //            ID = Convert.ToInt32(data[0]),
        //            Name = Convert.ToString(data[1]),
        //            InvNumber = Convert.ToString(data[2]),
        //            NeedsCheckcard = Convert.ToBoolean(data[3]),
        //            AnnualChecks = Convert.ToInt16(data[4]),
        //            KindOfCheck = Convert.ToString(data[5]),
        //            Comment = Convert.ToString(data[6]),
        //        };
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.StackTrace);
        //        return null;
        //    }

        //    return d;
        //}
    }

    public delegate void DataListChanged(object sender, EventArgs e);
}
