using System;
using System.Collections.Generic;
using System.Text;

namespace BuberDinner.Application.Command.service
{
    public interface IdataTimeProvider
    {

        DateTime UtcNow { get; }

    }
}
