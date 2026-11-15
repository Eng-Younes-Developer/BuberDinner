using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.services.Authintication
{
    public class AuthinticationResult
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        //constructor or record
        public AuthinticationResult(Guid id, string firstName, string lastName, string email, string token)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Token = token;
        }
   
    }
}
  

