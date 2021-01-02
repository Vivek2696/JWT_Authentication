using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI.Services.Queries
{
    public class CommandText: ICommandText
    {
        public string GetUserByEmail => "select * from Users where email = @Email";
        public string AddUser => "INSERT INTO [dbo].[Users] ([firstName],[lastName],[dob],[email]) VALUES (@FirstName, @LastName, @Dob, @Email)";
        public string AddPassword => "INSERT INTO [dbo].[Passwords] ([password],[password_salt],[password_hash_algorithm],[userId]) VALUES (@Password, @Password_Salt, @Password_Hash_Algorithm, @UserId)";
        public string GetPasswordById => "select * from Passwords where userId = @UserId";
    }
}
