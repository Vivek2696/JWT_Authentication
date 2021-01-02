using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using webAPI.Models;

namespace webAPI.Shared
{
    public sealed class PasswordHasher
    {
        public HashedPassword HashPassword(string password)
        {
            //generate salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //hash password with salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);

            //SavedHashPassword = salt + hashedPassword
            byte[] hashBytes = new byte[36];

            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            HashedPassword savedHashPassword = new HashedPassword
            {
                Salt = Convert.ToBase64String(salt),
                SavedPassword = Convert.ToBase64String(hashBytes)
            };

            return savedHashPassword;
        }

        public bool CheckPassword(string savedPassword, string newPassword)
        {
            //Hash the entered password for credential
            byte[] savedHashBytes = Convert.FromBase64String(savedPassword); //Saved Password

            //Get saved salt
            byte[] salt = new byte[16];
            Array.Copy(savedHashBytes, 0, salt, 0, 16);

            //input hashed password
            var pbkdf2 = new Rfc2898DeriveBytes(newPassword, salt, 1000);
            byte[] inputHashBytes = pbkdf2.GetBytes(20);

            //check if both password are same or not
            int same = 1;
            for(int i = 0; i < 20; i++)
            {
                if(savedHashBytes[i+16] != inputHashBytes[i])
                {
                    same = 0;
                }
            }
            if(same == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
