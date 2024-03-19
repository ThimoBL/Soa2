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
        private Guid Id { get; set; } = Guid.NewGuid();
        private string Title { get; set; } = title;
        private string Description { get; set; } = description;
        private int StoryPoints { get; set; } = storyPoints;
        private Developer Developer { get; set; } = developer;
        //ToDo: Status backlog item toevoegen

        private IEnumerable<Tasks>? Tasks { get; set; } = new List<Tasks>();

        //ToDo: Lijst van Threads toevoegen
    }
}
