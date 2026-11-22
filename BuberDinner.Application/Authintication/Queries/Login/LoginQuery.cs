using BuberDinner.Application.Authintication.ResponseAuth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.Authintication.Queries.Login
{
    public record LoginQuery
    (
       string Email,
       string Password
    )  : IRequest<AuthinticationResult>;

}
