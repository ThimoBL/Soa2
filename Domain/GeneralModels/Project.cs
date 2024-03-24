using Domain.Backlogs;
using Domain.Notifications.Interfaces;
using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Domain.VersionControl.Interfaces;

namespace Domain.GeneralModels
{
    public class Project(string title, string description, ProductOwner owner, VersionControlTypes gitType,
        ISprintFactory sprintFactory,
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
            Tester tester, Pipeline pipeline, IGitStrategy gitStrategy, SprintType sprintType,
            INotificationService notificationService)
        {
            var sprint = sprintFactory.CreateSprint(title, startDate, endDate, scrumMaster, tester, pipeline,
                gitStrategy, this, sprintType, notificationService);
            Sprints.Add(sprint);
        }
    }
}