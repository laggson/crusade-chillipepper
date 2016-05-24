using FWA.Gui.Content;

namespace FWA.Gui.Logic
{
    /// <summary>
    /// Die Kategorie, nach der Gegenstände in die Tabs einsortiert werden.
    /// </summary>
    public class DeviceCategory
    {
        /// <summary>
        /// Der Name des Tabs
        /// </summary>
        public string DisplayName { get; }
        /// <summary>
        /// Der Teil der Inventar Nummer, nach dem die Gegenstände in die Tabs einsortiert werden
        /// </summary>
        public string InvNumberLike { get; }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="DeviceCategory"/>-Klasse
        /// </summary>
        /// <param name="displayName">Der Anzeigename des Tabs</param>
        /// <param name="invNumberLike">Der Teil der Inventar Nummer, nach dem die Gegenstände in die Tabs einsortiert werden</param>
        public DeviceCategory(string displayName, string invNumberLike)
        {
            DisplayName = displayName;
            InvNumberLike = invNumberLike;
        }
    }
}
