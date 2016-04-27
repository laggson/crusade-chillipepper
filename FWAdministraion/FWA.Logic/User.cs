namespace FWA.Logic
{
    public class User
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
    }
}
