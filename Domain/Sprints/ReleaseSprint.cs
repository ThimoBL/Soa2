using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.Visitor;
using Domain.VersionControl;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints
{
    public class ReleaseSprint : Sprint
    {
        public ReleaseSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Pipeline pipeline, IGitStrategy gitStrategy) : base(title, startDate, endDate, scrumMaster, pipeline,
            gitStrategy)
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

        public override void RunPipeline()
        {
            Console.WriteLine($"=-=-=-= {Pipeline.Name} starting... =-=-=-=");
            Pipeline.AcceptPipeline(new PipelineVisitor());
        }
    }
}