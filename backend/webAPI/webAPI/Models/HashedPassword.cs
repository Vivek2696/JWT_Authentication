using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webAPI.Models
{
    public class HashedPassword
    {
        public string Salt { get; set; }
        public string SavedPassword { get; set; }
    }
}
