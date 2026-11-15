using BuberDinner.Contracts.Authintication;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.services.Authintication
{
    public interface IAuthinticationService
    {

        AuthinticationResult Register(string firstName, string lastName, string email, string password);

        AuthinticationResult Login(string email, string password);


    }
}
