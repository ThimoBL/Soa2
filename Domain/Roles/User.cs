using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Roles
{
    public class User(string name, string email, string password)
    {
        private Guid Id { get; set; } = Guid.NewGuid();
        private string Name { get; set; } = name;
        private string Email { get; set; } = email;
        private string Password { get; set; } = password;
    }
}
