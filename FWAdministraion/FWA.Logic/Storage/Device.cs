using System;
using System.ComponentModel;

namespace FWA.Logic.Storage
{
    /// <summary>
    /// Storage class for controlling the data of one single object, fitting for all logistical places.
    /// </summary>
    public class Device : IStorageItem
    {
        private int[] _monthsToCheck;

        public override string ToString()
        {
            return Name;
        }

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

        [DisplayName("Name")]
        public virtual string Name
        {
            get; set;
        }
        
        [DisplayName("Inventar-Nr.")]
        public virtual string InvNumber
        {
            get; set;
        }

        [DisplayName("Prüfkarte")]
        public virtual bool NeedsCheckcard
        {
            get; set;
        }

        [DisplayName("Jährl. Prüfungen")]
        public virtual short AnnualChecks
        {
            get; set;
        }

        [DisplayName("Durchzuf. Prüfung")]
        public virtual string KindOfCheck
        {
            get; set;
        }

        [DisplayName("Bemerkung")]
        public virtual string Comment
        {
            get; set;
        }

        protected virtual string GetLocation()
        {
            return InvNumber?.Split(' ')[1];
        }
    }
}
