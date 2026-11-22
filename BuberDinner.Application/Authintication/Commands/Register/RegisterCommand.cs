using BuberDinner.Application.Authintication.ResponseAuth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.Authintication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<AuthinticationResult>;
}
