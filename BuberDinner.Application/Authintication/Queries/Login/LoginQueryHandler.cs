using BuberDinner.Application.Authintication.ResponseAuth;
using BuberDinner.Application.Command.interfaces;
using BuberDinner.Application.Command.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.Authintication.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthinticationResult>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly IUserRepositiory _userRepositiory;

        public LoginQueryHandler( IJwtGenerator jwtGenerator, IUserRepositiory userRepositiory)
        {
            _jwtGenerator = jwtGenerator;
            _userRepositiory = userRepositiory;
        }
        public async Task<AuthinticationResult> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            var user = _userRepositiory.UserByEmail(request.Email);

            if (user == null)
            {
                throw new Exception("User with this email does not exist");
            }

            if (user.Password != request.Password)
            {
                throw new Exception("Invalid password");
            }

            var token = _jwtGenerator.GenerateToken(user);

            return new AuthinticationResult(user, token);
        }
 
    }
}
