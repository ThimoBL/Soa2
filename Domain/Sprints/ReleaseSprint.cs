using Domain.GeneralModels;
using Domain.Notifications.Interfaces;
using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.Visitor;
using Domain.VersionControl;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints
{
    public class ReleaseSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Tester tester, Pipeline pipeline, IGitStrategy gitStrategy, Project project, INotificationService notificationService)
        : Sprint(title, startDate, endDate, scrumMaster, tester, pipeline, gitStrategy, project, notificationService)
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
            Console.WriteLine($"=-=-=-= {Pipeline.Name} starting..   . =-=-=-=");
            Pipeline.AcceptPipeline(new PipelineVisitor());
        }

        public override void UploadReview()
        {
            throw new InvalidOperationException("Release sprint does not have a review");
        }
    }
}