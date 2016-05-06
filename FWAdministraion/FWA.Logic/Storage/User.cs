namespace FWA.Logic.Storage
{
    /// <summary>
    /// Storage class for controlling all data belonging to one user
    /// </summary>
    public class User : IStorageItem
    {

        public virtual int ID
        {
            get; set;
        }
        
        public virtual string Name
        {
            get; set;
        }

        public virtual string EMail
        {
            get; set;
        }

        public virtual string Hash
        {
            
            get; set;
        }

        public virtual string Salt
        {
            get; set;
        }

        public virtual AccountType AccountType
        {
            get; set;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }

    public enum AccountType
    {
        Master, User, Spectator
    }
}
