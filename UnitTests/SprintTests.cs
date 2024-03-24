using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Pipelines.Actions.BuildAction;
using Domain.Pipelines.Actions.PackageAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Pipelines.Actions.TestAction;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;
using Domain.Sprints.States;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Moq;
using System.Diagnostics.Metrics;

namespace UnitTests
{
    public class SprintTests
    {
        [Fact]
        public void ReleaseSprint_Closed_After_FinishedState()
        {
            // Arrange
            // _project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), _scrumMaster,
                // new Pipeline("Pipeline 1"), SprintType.Release, new GitStrategy());
            // var releaseSprint = _project.Sprints.First();
            // releaseSprint.ChangeState(new FinishedState(releaseSprint));

            // Act
            // releaseSprint.NextSprintState();

            // Assert
            // Assert.IsType<ClosedState>(releaseSprint.SprintState);
        }

        [Fact]
        public void Run_Pipeline_When_ReleaseSprint_Is_Done()
        {
            //Arrange
            var tester = new Tester("John Doe", "Johndoe@email.com", "password");

            var mockPipeline = new Mock<Pipeline>("Pipeline 1");

            mockPipeline.Object.AddAction(Constants.SourceAction);
            mockPipeline.Object.AddAction(Constants.PackageAction);
            mockPipeline.Object.AddAction(Constants.BuildAction);
            mockPipeline.Object.AddAction(Constants.TestAction);
            mockPipeline.Object.AddAction(Constants.AnalyzeAction);
            mockPipeline.Object.AddAction(Constants.DeployAction);
            mockPipeline.Object.AddAction(Constants.UtilityAction);

            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(), new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                tester, mockPipeline.Object, new GitStrategy(), SprintType.Release);

            var releaseSprint = project.Sprints.First();

            //Act
            releaseSprint.ChangeState(new FinishedState(releaseSprint));

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
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(), new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                tester, pipeline, new GitStrategy(), SprintType.Review);

            var reviewSprint = project.Sprints.First();

            //Act
            reviewSprint.UploadReview();

            //Assert
            Assert.True(reviewSprint.IsReviewUploaded());
        }
    }
}