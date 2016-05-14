using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FWA.Logic.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException(string username, string msg)
            : base(string.Format("Failed to authenticate as User '{0}': {1}", username, msg))
        { }
    }
}
