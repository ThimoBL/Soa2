using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.Visitor;
using Domain.VersionControl;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints
{
    public class ReleaseSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Tester tester, Pipeline pipeline, IGitStrategy gitStrategy, Project project)
        : Sprint(title, startDate, endDate, scrumMaster, tester, pipeline, gitStrategy, project)
    {
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