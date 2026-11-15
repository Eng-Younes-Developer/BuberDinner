using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Contracts.Authintication
{
    public class LoginRequest
    {
       public string Email { get; set; }
        public string Password { get; set; }
    }
}
