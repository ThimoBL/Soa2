using Domain.Backlog;
using Domain.Roles;

namespace DomainService
{
    public class Project(string title, string description, ProductOwner owner, ProductBacklog backlog)
    {
        private Guid Id { get; set; } = Guid.NewGuid();
        private string Title { get; set; } = title;
        private string Description { get; set; } = description;
        private ProductOwner Owner { get; set; } = owner;
        private ProductBacklog ProductBacklog { get; set; } = backlog;
        //Add version control strategy/ Code archive

        //Add Pipelines

    }
}
