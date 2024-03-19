using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Roles
{
    public class ScrumMaster(string name, string email, string password) : User(name, email, password)
    {
    }
}
