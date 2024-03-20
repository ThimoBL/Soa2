using Domain.Backlog;
using Domain.Roles;
using Domain.Sprints;

namespace DomainService
{
    public class Project(string title, string description, ProductOwner owner, ProductBacklog backlog)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public ProductOwner Owner { get; set; } = owner;
        public ProductBacklog ProductBacklog { get; set; } = backlog;
        public IList<Sprint> Sprints { get; set; } = new List<Sprint>() ;
        //ToDo: Add version control strategy/ Code archive

        //ToDo: Add Pipelines

    }
}
