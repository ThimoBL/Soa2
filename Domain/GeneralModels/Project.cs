using Domain.Backlogs;
using Domain.Pipelines;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;

namespace Domain.GeneralModels
{
    public class Project(string title, string description, ProductOwner owner, ISprintFactory sprintFactory)
    {
        private readonly ISprintFactory _sprintFactory = sprintFactory;
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public ProductOwner Owner { get; set; } = owner;
        public List<Backlog> ProductBacklog { get; set; } = new();
        public IList<Sprint> Sprints { get; set; } = new List<Sprint>();

        //ToDo: Add version control strategy/ Code archive

        public void AddBacklog(Backlog backlog)
        {
            ProductBacklog.Add(backlog);
        }

        public void CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Pipeline pipeline, SprintType sprintType)
        {
            var sprint = _sprintFactory.CreateSprint(title, startDate, endDate, scrumMaster, pipeline, sprintType);
            Sprints.Add(sprint);
        }
    }
}