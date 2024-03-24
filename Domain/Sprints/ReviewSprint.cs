using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.Visitor;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints
{
    public class ReviewSprint : Sprint
    {
        public ReviewSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster, Tester tester,
            Pipeline pipeline, IGitStrategy gitStrategy, Project project) : base(title, startDate, endDate, scrumMaster,
            tester, pipeline, gitStrategy, project)
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