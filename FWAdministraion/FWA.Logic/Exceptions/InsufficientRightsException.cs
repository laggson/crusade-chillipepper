using FWA.Logic.Storage;
using System;

namespace FWA.Logic
{
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

        public User User { get; set; }

        public AccountType Needed { get; set; }

        public string Action { get; set; }

        public InsufficientRightsException(User user, AccountType needed, string action) 
            : base(GenerateMessage(user, needed, action))
        {
            User = user;
            Needed = needed;
            Action = action;
        }
    }
}
