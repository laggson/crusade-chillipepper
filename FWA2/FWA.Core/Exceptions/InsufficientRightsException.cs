using FWA.Core.Models;
using System;

namespace FWA.Core.Exceptions
{
    /// <summary>
    /// Die Ausnahme wird ausgelöst, falls ein Benutzer versucht eine Aktion auszuführen, für die sein Rechte-Level zu gering ist.
    /// </summary>
    public class InsufficientRightsException : Exception
    {
        private static string GenerateMessage(User user, AccountType needed, string action)
        {
            return string.Format("User '{0}' has insufficient permissions: Actual {1}, needed {2}{3}Action :{4}",
                user?.Name ?? "*unknown*", 
                user == null ? "*unknown*" : user.AccountType.ToString(), 
                needed, Environment.NewLine,
                string.IsNullOrWhiteSpace(action) ? "*unknown*" : action);
        }

        /// <summary>
        /// Der Nutzer, der versucht die Aktion auszuführen
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Das Minimum des Rechte-Levels, das für die gewählte Aktion vorhanden sein muss
        /// </summary>
        public AccountType Needed { get; set; }

        /// <summary>
        /// Die Aktion, die der Nutzer versuchte auszuführen
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Initialisiert eine neue Instanz der <see cref="InsufficientRightsException"/>
        /// </summary>
        /// <param name="user">Der Nutzer, der versucht die Aktion auszuführen</param>
        /// <param name="needed">Das Minimum des Rechte-Levels, das für die gewählte Aktion vorhanden sein muss</param>
        /// <param name="action">Die Aktion, die der Nutzer versuchte auszuführen</param>
        public InsufficientRightsException(User user, AccountType needed, string action) 
            : base(GenerateMessage(user, needed, action))
        {
            User = user;
            Needed = needed;
            Action = action;
        }
    }
}
