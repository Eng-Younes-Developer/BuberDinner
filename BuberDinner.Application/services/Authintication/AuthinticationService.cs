using BuberDinner.Application.Command.interfaces;
using BuberDinner.Contracts.Authintication;
using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using BuberDinner.Application.Command.Persistence;
using System.Text;

namespace BuberDinner.Application.services.Authintication
{

    public class AuthinticationService : IAuthinticationService
    {
        private readonly IJwtGenerator _jwtGenerator;

        private readonly IUserRepositiory _userRepositiory;
        public AuthinticationService(IJwtGenerator jwtGenerator, IUserRepositiory userRepositiory)
        {
            _jwtGenerator = jwtGenerator;
            _userRepositiory = userRepositiory;
        }
        public AuthinticationResult Login(string email, string password)
        {
            var user = _userRepositiory.UserByEmail(email);
            if (user == null)
            {
                throw new Exception("User with this email does not exist");
            }
            var token = _jwtGenerator.GenerateToken(user);
            return new AuthinticationResult(user, token);
        }

        public AuthinticationResult Register(string firstName, string lastName, string email, string password)
        {
            //check if user already exists 

            var existingUser = _userRepositiory.UserByEmail(email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }
            // create user
            User user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            _userRepositiory.AddUser(user);
            // create jwt token

            var token = _jwtGenerator.GenerateToken(user);

            return new AuthinticationResult(user, token);
        }
    }
}
