using BuberDinner.Application.Command.interfaces;
using BuberDinner.Contracts.Authintication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.services.Authintication
{
    
    public class AuthinticationService : IAuthinticationService
    {
        private readonly IJwtGenerator _jwtGenerator;

        public AuthinticationService(IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
        }
        public AuthinticationResult Login(string email, string password)
        {
            return new AuthinticationResult(Guid.NewGuid(), "FirstName", "LastName", email, "Token");
        }

        public AuthinticationResult Register(string firstName, string lastName, string email, string password)
        {
            //check if user already exists 
            // create user
            // create jwt token

            var userId = Guid.NewGuid();
            var token = _jwtGenerator.GenerateToken(userId, firstName, lastName);

            return new AuthinticationResult(userId, firstName, lastName, email, token);
        }
    }
}
