using Domain.Backlogs;
using Domain.Pipelines;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Domain.VersionControl.Interfaces;

namespace Domain.GeneralModels
{
    public class Project(string title, string description, ProductOwner owner, VersionControlTypes gitType, ISprintFactory sprintFactory,
        IVersionControlFactory versionControlFactory)
    {
        private IGitStrategy GitStrategy { get; set; } = versionControlFactory.CreateGitStrategy(gitType);
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public ProductOwner Owner { get; set; } = owner;
        public IList<BacklogItem> ProductBacklog { get; set; } = new List<BacklogItem>();
        public IList<Sprint> Sprints { get; set; } = new List<Sprint>();

        public void AddBacklog(BacklogItem backlogItem)
        {
            ProductBacklog.Add(backlogItem);
        }

        public IGitStrategy GetGitStrategy() => GitStrategy;

        public void CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Pipeline pipeline, SprintType sprintType, IGitStrategy gitStrategy)
        {
            var sprint = sprintFactory.CreateSprint(title, startDate, endDate, scrumMaster, pipeline, sprintType, gitStrategy);
            Sprints.Add(sprint);
        }
    }
}