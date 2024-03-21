using Domain.Backlogs;
using Domain.Roles;
using Domain.Sprints.States;

namespace Domain.Sprints
{
    public abstract class Sprint
    {
        protected Sprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster)
        {
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
            ScrumMaster = scrumMaster;
            SprintState = new OpenState(this);
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Backlog SprintBacklog { get; set; } = new();
        public ScrumMaster ScrumMaster { get; set; }
        public SprintState SprintState { get; set; }

        public void ChangeState(SprintState sprintState)
        {
            SprintState = sprintState;
        }

        internal abstract void Accept(ISprintVisitor visitor);
        public abstract void NextSprintState();
    }
}