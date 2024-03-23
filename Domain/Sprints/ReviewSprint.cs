using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.Visitor;

namespace Domain.Sprints
{
    internal class ReviewSprint : Sprint
    {
        //ToDo: add reviews to sprint

        //ToDo: add optional pipeline to sprint (maybe for review sprint but is an assumption)
        public ReviewSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Pipeline pipeline) : base(title,
            startDate, endDate, scrumMaster, pipeline)
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

        public override void RunPipeline()
        {
            throw new NotImplementedException();
        }
    }
}