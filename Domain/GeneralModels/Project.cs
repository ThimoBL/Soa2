using Domain.Backlogs;
using Domain.Pipelines;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;

namespace Domain.GeneralModels
{
    public class Project(string title, string description, ProductOwner owner, ISprintFactory sprintFactory)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public ProductOwner Owner { get; set; } = owner;
        public IList<BacklogItem> ProductBacklog { get; set; } = new List<BacklogItem>();
        public IList<Sprint> Sprints { get; set; } = new List<Sprint>();

        //ToDo: Add version control strategy/ Code archive

        public void AddBacklog(BacklogItem backlogItem)
        {
            ProductBacklog.Add(backlogItem);
        }

        public void CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Pipeline pipeline, SprintType sprintType)
        {
            var sprint = sprintFactory.CreateSprint(title, startDate, endDate, scrumMaster, pipeline, sprintType);
            Sprints.Add(sprint);
        }
    }
}