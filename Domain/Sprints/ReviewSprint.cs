using Domain.GeneralModels;
using Domain.Notifications.Interfaces;
using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints.Visitor;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints
{
    public class ReviewSprint : Sprint
    {
        private bool ReviewUploaded { get; set; }

        public ReviewSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster, Tester tester,
            Pipeline pipeline, IGitStrategy gitStrategy, Project project,
            INotificationService notificationService) : base(title, startDate, endDate, scrumMaster, tester, pipeline,
            gitStrategy, project, notificationService)
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

        public override void UploadReview()
        {
            ReviewUploaded = true;
            Console.WriteLine("Review uploaded");
        }

        public override bool IsReviewUploaded() => ReviewUploaded;
    }
}