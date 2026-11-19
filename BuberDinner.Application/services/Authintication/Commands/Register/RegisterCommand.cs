using BuberDinner.Application.services.Authintication.ResponseAuth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.services.Authintication.Commands.Register
{
    public record RegisterCommand(
        string FirstName,
        string LastName,
        string Email,
        string Password
    ) : IRequest<AuthinticationResult>;
}
