using BuberDinner.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.Command.Persistence
{
    public interface IUserRepositiory
    {
        User? UserByEmail(string email);
        public void AddUser(User user);

    }
}
