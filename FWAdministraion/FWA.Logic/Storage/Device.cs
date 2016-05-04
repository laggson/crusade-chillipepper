﻿using System.ComponentModel;

namespace FWA.Logic.Storage
{
    /// <summary>
    /// Storage class for controlling the data of one single object, fitting for all logistical places.
    /// </summary>
    public class Device : IStorageItem
    {

        public override string ToString()
        {
            string s = ID + ";" + Name + ";" + InvNumber + ";" + NeedsCheckcard + ";" + AnnualChecks + ";";

            return s;
        }

        [DisplayName("ID")]
        public virtual int ID
        {
            get; set;
        }

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