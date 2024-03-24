using Domain.Roles;
using Domain.Sprints;

namespace Domain.Backlogs
{
    public class BacklogItem(string title, string description, int storyPoints, Developer developer, Sprint sprint) : Item(title,
        description, developer, sprint)
    {
        //Navigation property
        public int StoryPoints { get; set; } = storyPoints;
        public IList<Task> Tasks { get; set; } = new List<Task>();

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }
    }
}