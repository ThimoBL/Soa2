using Domain.Backlogs;
using Domain.Pipelines;
using Domain.Roles;
using Domain.Sprints.States;
using Domain.Sprints.Visitor;

namespace Domain.Sprints
{
    public abstract class Sprint
    {
        private Pipeline _pipeline;
        protected Sprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster, Pipeline pipeline)
        {
            Title = title;
            StartDate = startDate;
            EndDate = endDate;
            ScrumMaster = scrumMaster;
            SprintState = new OpenState(this);

            _pipeline = pipeline;
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

        internal abstract void AcceptSprint(ISprintVisitor visitor);
        public abstract void NextSprintState();
    }
}