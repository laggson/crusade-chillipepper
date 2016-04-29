namespace FWA.Logic.Storage
{
    /// <summary>
    /// Storage class for controlling the data of one single object, fitting for all logistical places.
    /// </summary>
    public class Device : IStorageItem
    {

        public virtual int ID
        {
            get; set;
        }

        public virtual string Name
        {
            get; set;
        }

        public virtual string InvNumber
        {
            get; set;
        }

        public virtual bool NeedsCheckcard
        {
            get; set;
        }

        public virtual short AnnualChecks
        {
            get; set;
        }

        public string GetLocation()
        {
            return InvNumber.Split(' ')[1];
        }
    }
}
