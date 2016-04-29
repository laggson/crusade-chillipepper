namespace FWA.Logic.Storage
{
    interface IStorageItem
    {
        int ID
        {
            get; set;
        }

        string Name
        {
            get; set;
        }
    }
}
