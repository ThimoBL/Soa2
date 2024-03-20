using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlog.States;
using Domain.Roles;

namespace Domain.Backlog
{
    public class BacklogItem(string title, string description, int storyPoints, Developer developer) : Item(title,
        description, developer)
    {
        public int StoryPoints { get; set; } = storyPoints;
        public IEnumerable<Task>? Tasks { get; set; } = new List<Task>();

        //ToDo: Lijst van Threads toevoegen
    }
}