using BuberDinner.Contracts.Authintication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.services.Authintication
{
    public class AuthinticationService : IAuthinticationService
    {
        public AuthinticationResult Login(string email, string password)
        {
            return new AuthinticationResult(Guid.NewGuid(), "FirstName", "LastName", email, "Token");
        }

        public AuthinticationResult Register(string firstName, string lastName, string email, string password)
        {
            return new AuthinticationResult( Guid.NewGuid(), firstName, lastName , email, "Token");
        }
    }
}
