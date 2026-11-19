using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.services.Authintication.ResponseAuth
{
    public record AuthinticationResult 
    (
        User user,
        string Token
    );

}


