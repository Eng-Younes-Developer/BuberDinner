using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.Command.interfaces
{
    public interface IJwtGenerator
    {

        public string GenerateToken(User user);
    }
}
