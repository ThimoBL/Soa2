using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.Visitor;

namespace Domain.Sprints
{
    public class ReleaseSprint : Sprint
    {
        //ToDo: add pipeline to sprint
        public ReleaseSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Pipeline pipeline) : base(title,
            startDate, endDate, scrumMaster, pipeline)
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