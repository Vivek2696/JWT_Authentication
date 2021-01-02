using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using webAPI.Models;
using webAPI.Services;
using webAPI.Shared;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private PasswordHasher passwordHasher;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        // POST api/<AuthController>
        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Authenticate([FromBody] LoginModel credential)
        {
            if (credential.Email == null || credential.Password == null)
            {
                return BadRequest();
            }
            //Find User from the database
            User currentUser = await _authRepository.GetUserByEmail(credential.Email);

            if(currentUser == null)
            {
                return NotFound();
            }

            passwordHasher = new PasswordHasher();
            PasswordModel savedPassword = await _authRepository.GetPasswordById(currentUser.Id);

            //Verify and generate JWT
            if (passwordHasher.CheckPassword(savedPassword.Password, credential.Password))
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretJWTKey114"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenOptions = new JwtSecurityToken(
                    issuer: "http://localhost:4200",
                    audience: "http://localhost:4200",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: signingCredentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                var result = new
                {
                    fistName = currentUser.FirstName,
                    lastName = currentUser.LastName,
                    email = currentUser.Email,
                    jwt = tokenString
                };

                return Ok(result);
            }

            return NotFound();

        }

    }
}
