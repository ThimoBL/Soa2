using Domain.Backlogs;
using Domain.Roles;
using Domain.Sprints;

namespace Domain
{
    public class Project(string title, string description, ProductOwner owner)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public ProductOwner Owner { get; set; } = owner;
        public List<Backlog> ProductBacklog { get; set; } = new();
        public IList<Sprint> Sprints { get; set; } = new List<Sprint>();
        //ToDo: Add version control strategy/ Code archive

        //ToDo: Add Pipelines

    }
}
