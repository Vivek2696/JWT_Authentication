using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAPI.Models;
using webAPI.Services.Queries;

namespace webAPI.Services
{
    public class AuthRepository: BaseRepository, IAuthRepository
    {
        private readonly ICommandText _commandText;

        public AuthRepository(IConfiguration configuration, ICommandText commandText) : base(configuration)
        {
            _commandText = commandText;
        }

        public async ValueTask<User> GetUserByEmail(string email)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryFirstOrDefaultAsync<User>(_commandText.GetUserByEmail, new { Email = email });
                return query;
            });
        }

        public async Task AddUser(AccountModel entity)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.AddUser,
                    new
                    {
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Dob = entity.Dob,
                        Email = entity.Email
                    });
            });
        }

        public async Task AddPassword(PasswordModel entity)
        {
            await WithConnection(async conn =>
            {
                await conn.ExecuteAsync(_commandText.AddPassword,
                    new
                    {
                        Password = entity.Password,
                        Password_Salt = entity.Password_Salt,
                        Password_Hash_Algorithm = entity.Password_Hash_Algorithm,
                        UserId = entity.UserId
                    });
            });
        }

        public async ValueTask<PasswordModel> GetPasswordById(int userId)
        {
            return await WithConnection(async conn =>
            {
                var query = await conn.QueryFirstOrDefaultAsync<PasswordModel>(_commandText.GetPasswordById, new { UserId = userId });
                return query;
            });
        }
    }
}
