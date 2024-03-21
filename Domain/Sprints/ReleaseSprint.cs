using Domain.Roles;

namespace Domain.Sprints
{
    public class ReleaseSprint : Sprint
    {
        //ToDo: add pipeline to sprint
        public ReleaseSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster) : base(title,
            startDate, endDate, scrumMaster)
        {
        }

        internal override void Accept(ISprintVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void NextSprintState()
        {
            this.SprintState.NextState();
        }
    }
}