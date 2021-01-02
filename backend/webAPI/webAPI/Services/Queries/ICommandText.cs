using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI.Services.Queries
{
    public interface ICommandText
    {
        string GetUserByEmail { get; }
        string AddUser { get; }
        string AddPassword { get; }
        string GetPasswordById { get; }
    }
}
