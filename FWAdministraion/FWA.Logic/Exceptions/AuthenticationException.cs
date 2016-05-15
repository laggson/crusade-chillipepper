using System;

namespace FWA.Logic.Exceptions
{
    public class AuthenticationException : Exception
    {
        public string Username { get; }

        public string FailReasonMessage { get; }

        public AuthenticationException(string username, string msg)
            : base(string.Format("Failed to authenticate as User '{0}': {1}", username, msg))
        {
            Username = username;
            FailReasonMessage = msg;
        }
    }
}
