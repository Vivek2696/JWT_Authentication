using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI.Models
{
    public class PasswordModel
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Password_Salt { get; set; }
        public string Password_Hash_Algorithm { get; set; }
        public int UserId { get; set; }
    }
}
