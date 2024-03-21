using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;

namespace Domain.Backlogs
{
    public class Task : Item
    {
        public Task(string title, string description, Developer developer) : base(title, description, developer)
        {
        }
    }
}
