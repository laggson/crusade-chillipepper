namespace FWA.Gui.Logic
{
    /// <summary>
    /// Contains the Category of an device and how the invNumber is like, for specifying the tab it is saved in
    /// </summary>
    public class DeviceCategory
    {
        public string DisplayName { get; }
        public string InvNumberLike { get; }

        public DeviceCategory(string displayName, string invNumberLike)
        {
            DisplayName = displayName;
            InvNumberLike = invNumberLike;
        }
    }
}
