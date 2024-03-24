using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Pipelines.Actions.AnalyzeAction.SonarCube;
using Domain.Pipelines.Actions.AnalyzeAction;
using Domain.Pipelines.Actions.BuildAction;
using Domain.Pipelines.Actions.DeployAction;
using Domain.Pipelines.Actions.PackageAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Pipelines.Actions.TestAction;
using Domain.Pipelines.Actions.UtilityAction;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;
using Domain.Sprints.States;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Moq;
using System.Diagnostics.Metrics;
using Domain.Notifications;

namespace UnitTests
{
    public class SprintTests
    {
        [Fact]
        public void CreateSprint_With_ReviewType()
        {
            // Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            // Act
            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                Constants.ExampleTester, pipeline, new GitStrategy(), SprintType.Review, new NotificationService());

            var reviewSprint = project.Sprints.First();

            // Assert
            Assert.Equal(typeof(ReviewSprint), reviewSprint.GetType());
        }

        [Fact]
        public void CreateSprint_With_ReleaseType()
        {
            // Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            // Act
            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                Constants.ExampleTester, pipeline, new GitStrategy(), SprintType.Release, new NotificationService());

            var releaseSprint = project.Sprints.First();

            // Assert
            Assert.Equal(typeof(ReleaseSprint), releaseSprint.GetType());
        }

        [Fact]
        public void ReleaseSprint_Closed_After_FinishedState()
        {
            // Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                Constants.ExampleTester, pipeline, new GitStrategy(), SprintType.Release, new NotificationService());

            var releaseSprint = project.Sprints.First();

            releaseSprint.ChangeState(new FinishedState(releaseSprint));

            // Act
            releaseSprint.NextSprintState();
            releaseSprint.NextSprintState();

            // Assert
            Assert.IsType<ClosedState>(releaseSprint.SprintState);
        }

        [Fact]
        public void Run_Pipeline_When_ReleaseSprint_Is_Done()
        {
            //Arrange
            var tester = new Tester("John Doe", "Johndoe@email.com", "password");

            var mockPipeline = new Mock<Pipeline>("Pipeline 1");

            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                tester, mockPipeline.Object, new GitStrategy(), SprintType.Release, new NotificationService());

            var releaseSprint = project.Sprints.First();

            //Act
            releaseSprint.NextSprintState(); // PlanningState
            releaseSprint.NextSprintState(); // ActiveState
            releaseSprint.NextSprintState(); // FinishedState
            releaseSprint.NextSprintState(); // ReleaseState
            releaseSprint.NextSprintState(); // ClosedState

            //Assert
            mockPipeline.Verify(p => p.AcceptPipeline(It.IsAny<PipelineVisitor>()), Times.Once);
        }

        [Fact]
        public void ReviewSprint_Needs_Extra_Action()
        {
            //Arrange
            var tester = new Tester("John Doe", "Johndoe@email.com", "password");
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                tester, pipeline, new GitStrategy(), SprintType.Review, new NotificationService());

            var reviewSprint = project.Sprints.First();

            //Act
            reviewSprint.UploadReview();

            //Assert
            Assert.True(reviewSprint.IsReviewUploaded());
        }

        [Fact]
        public void Cant_Review_Release_Sprint()
        {
            //Arrange
            var tester = new Tester("John Doe", "Johndoe@email.com", "password");
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                tester, pipeline, new GitStrategy(), SprintType.Release, new NotificationService());

            var reviewSprint = project.Sprints.First();

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => reviewSprint.UploadReview());
        }
    }
}