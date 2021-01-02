using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.Services
{
    public interface IAuthRepository
    {
        ValueTask<User> GetUserByEmail(string email);
        Task AddUser(AccountModel entity);
        Task AddPassword(PasswordModel entity);
        ValueTask<PasswordModel> GetPasswordById(int userId);
    }
}
