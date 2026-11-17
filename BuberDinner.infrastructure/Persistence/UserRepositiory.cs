using System;
using System.Collections.Generic;
using System.Text;
using BuberDinner.Application.Command.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.infrastructure.Persistence
{
    public class UserRepositiory : IUserRepositiory
    {
        private static readonly List<User> _users = new();
        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public User? UserByEmail(string email)
        {
            return _users.FirstOrDefault(u => u.Email == email);
        }
    }
}
