using System;

namespace FWA.Logic.Exceptions
{
    public class AuthenticationException : Exception
    {
        public string Username { get; }

        public string FailReasonMessage { get; }

        public AuthenticationException(string username, string msg)
            : base(string.Format("Anmeldung als Nutzer '{0}' fehlgeschlagen: {1}", username, msg))
        {
            Username = username;
            FailReasonMessage = msg;
        }
    }
}
