namespace FWA.Gui.Logic
{
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
