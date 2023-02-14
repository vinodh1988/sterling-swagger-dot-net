using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace SterlingAPI.Authentication
{
    public class AuthIdentity : GenericIdentity
    {
        public string Password { get; set; }
        public AuthIdentity(string name, string password) : base(name, "Basic")
        {
            this.Password = password;
        }
    }
   
}