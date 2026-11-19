using BuberDinner.Application.services.Authintication.ResponseAuth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.services.Authintication.Queries.Login
{
    public record LoginQuery
    (
       string Email,
       string Password
    )  : IRequest<AuthinticationResult>;

}
