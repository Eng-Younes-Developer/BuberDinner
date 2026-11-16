using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.Command.interfaces
{
    public interface IJwtGenerator
    {

        public string GenerateToken(Guid userId, string firstName, string lastName);
    }
}
