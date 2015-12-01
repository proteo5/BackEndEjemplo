using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteri.Lib.DTO
{
    public class UsersDTO
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string UserNames { get; set; }
        public string UsersLastNames { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}
