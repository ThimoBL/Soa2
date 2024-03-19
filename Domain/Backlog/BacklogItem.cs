using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;

namespace Domain.Backlog
{
    public class BacklogItem(string title, string description, int storyPoints, Developer developer)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public int StoryPoints { get; set; } = storyPoints;
        public Developer Developer { get; set; } = developer;
        //ToDo: Status backlog item toevoegen

        public IEnumerable<Tasks>? Tasks { get; set; } = new List<Tasks>();

        //ToDo: Lijst van Threads toevoegen
    }
}
