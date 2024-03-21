using Domain.Roles;
using Domain.Sprints.Visitor;

namespace Domain.Sprints
{
    public class ReleaseSprint : Sprint
    {
        //ToDo: add pipeline to sprint
        public ReleaseSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster) : base(title,
            startDate, endDate, scrumMaster)
        {
        }

        internal override void AcceptSprint(ISprintVisitor visitor)
        {
            visitor.VisitRelease(this);
        }

        public override void NextSprintState()
        {
            this.SprintState.NextState();
        }
    }
}