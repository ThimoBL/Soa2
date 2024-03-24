using Domain.Backlogs;
using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.States;
using Domain.Sprints.Visitor;
using System.Xml.Linq;
using Domain.GeneralModels;
using Domain.Notifications;
using Domain.Notifications.Interfaces;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints
{
    public abstract class Sprint
    {
        private readonly Project _project;
        private readonly INotificationService _notificationService;

        protected Sprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster, Tester tester,
            Pipeline pipeline, IGitStrategy gitStrategy, Project project, INotificationService notificationService)
        {
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
            Tester = tester;
            ScrumMaster = scrumMaster;
            SprintState = new OpenState(this);

            Pipeline = pipeline;
            GitStrategy = gitStrategy;

            //Navigation properties
            _project = project;
            _notificationService = notificationService;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IList<BacklogItem> SprintBacklogItems { get; set; } = new List<BacklogItem>();
        public ScrumMaster ScrumMaster { get; set; }
        public Tester Tester { get; set; }
        public SprintState SprintState { get; set; }
        public Pipeline Pipeline { get; set; }
        public IGitStrategy GitStrategy { get; set; }

        public void ChangeState(SprintState sprintState)
        {
            SprintState = sprintState;
        }

        public void AddBacklogItem(BacklogItem backlogItem)
        {
            SprintBacklogItems.Add(backlogItem);
        }

        internal abstract void AcceptSprint(ISprintVisitor visitor);
        public abstract void NextSprintState();
        public abstract void RunPipeline();

        public abstract void UploadReview();

        public virtual bool IsReviewUploaded() => false;

        public virtual void NotifyTester(string message) =>
            _notificationService.SendSingleNotification(Tester, message);


        public virtual void NotifyScrumMaster(string message) =>
            _notificationService.SendSingleNotification(ScrumMaster, message);
    }
}