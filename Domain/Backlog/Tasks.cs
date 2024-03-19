using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;

namespace Domain.Backlog
{
    public class Tasks
    {
        private Guid Id { get; set; }
        private string Title { get; set; }
        private string Description { get; set; }
        private Developer Developer { get; set; }
    }
}
