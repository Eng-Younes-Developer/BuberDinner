using BuberDinner.Application.Command.interfaces;
using BuberDinner.Application.Command.Persistence;
using BuberDinner.Application.services.Authintication.Commands.Register;
using BuberDinner.Application.services.Authintication.ResponseAuth;
using BuberDinner.Contracts.Authintication;
using BuberDinner.Domain.Entities;
using MediatR;
using System;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;


namespace BuberDinner.Application.services.Authintication.Commands.RegisterCommandHandler
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthinticationResult>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserRepositiory _userRepositiory;
        public RegisterCommandHandler( IJwtGenerator jwtGenerator, IUserRepositiory userRepositiory)
        {
            _jwtGenerator = jwtGenerator;
            _userRepositiory = userRepositiory;
        }
        public async Task<AuthinticationResult> Handle(RegisterCommand Request, CancellationToken cancellationToken)
        {
            var existingUser = _userRepositiory.UserByEmail(Request.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists");
            }
            // create user
            var user = new User
            {
                FirstName = Request.FirstName,
                LastName = Request.LastName,
                Email = Request.Email,
                Password = Request.Password
            };

            _userRepositiory.AddUser(user);
            // create jwt token

            var token = _jwtGenerator.GenerateToken(user);

            return new AuthinticationResult(user, token);
        }   

    }
}
