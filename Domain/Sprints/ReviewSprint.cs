using Domain.Roles;
using Domain.Sprints.Visitor;

namespace Domain.Sprints
{
    internal class ReviewSprint : Sprint
    {
        //ToDo: add reviews to sprint

        //ToDo: add optional pipeline to sprint (maybe for review sprint but is an assumption)
        public ReviewSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster) : base(title,
            startDate, endDate, scrumMaster)
        {
        }
        internal override void AcceptSprint(ISprintVisitor visitor)
        {
            visitor.VisitReview(this);
        }

        public override void NextSprintState()
        {
            this.SprintState.NextState();
        }
    }
}