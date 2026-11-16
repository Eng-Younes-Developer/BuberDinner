using System;
using System.Collections.Generic;
using System.Text;
using BuberDinner.Application.Command.service;

namespace BuberDinner.infrastructure.Services
{
    public class dataTimeProvider : IdataTimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
