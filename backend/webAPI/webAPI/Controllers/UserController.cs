using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webAPI.Models;
using webAPI.Services;
using webAPI.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IAuthRepository _authRepository;
        private PasswordHasher _passwordHasher;

        public UserController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        // POST api/<UserController>
        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] AccountModel accountModel)
        {
            //Store the User detail in the User table
            await _authRepository.AddUser(accountModel);

            Console.WriteLine("User Added: ", accountModel.FirstName + accountModel.LastName);

            //Get the user id of the same user
            User new_user = await _authRepository.GetUserByEmail(accountModel.Email);

            //then store the hashed password
            _passwordHasher = new PasswordHasher();
            HashedPassword savedHashedPassword = _passwordHasher.HashPassword(accountModel.Password);

            PasswordModel passwordModel = new PasswordModel
            {
                Password = savedHashedPassword.SavedPassword,
                Password_Salt = savedHashedPassword.Salt,
                Password_Hash_Algorithm = "PBKDF2", //Default Hash Algorithm
                UserId = new_user.Id
            };

            await _authRepository.AddPassword(passwordModel);

            var result = new
            {
                message = "Regestered Successfully",
            };

            return Ok(result);
        }

    }
}
