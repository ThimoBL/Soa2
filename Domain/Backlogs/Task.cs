using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;
using Domain.Sprints;

namespace Domain.Backlogs
{
    public class Task : Item
    {
        public Task(string title, string description, Developer developer, Sprint sprint) : base(title, description,
            developer, sprint)
        {
        }

        //Navigation property
        private readonly BacklogItem _backlogItem;
    }
}