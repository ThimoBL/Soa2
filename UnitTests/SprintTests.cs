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

namespace UnitTests
{
    public class SprintTests
    {
        private readonly Project _project = new("Project Alpha", "This is a test project",
            new ProductOwner("Name", "Email", "Password"), VersionControlTypes.Git, new SprintFactory(),
            new VersionControlFactory());
        private readonly ScrumMaster _scrumMaster = new("John Doe", "Johndoe@email.nl", "password");


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
            var sourceAction = new SourceGithubAction();
            var packageAction = new PackageInstallAction();
            var buildAction = new BuildMavenAction();
            var testAction = new TestNUnitAction();

            mockPipeline.Object.AddAction(sourceAction);
            mockPipeline.Object.AddAction(packageAction);
            mockPipeline.Object.AddAction(buildAction);
            mockPipeline.Object.AddAction(testAction);

            _project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), _scrumMaster,
                tester, mockPipeline.Object, new GitStrategy(), SprintType.Release);

            var releaseSprint = _project.Sprints.First();

            //Act
            releaseSprint.ChangeState(new FinishedState(releaseSprint));

            //Assert
            mockPipeline.Verify(p => p.AcceptPipeline(It.IsAny<PipelineVisitor>()), Times.Once);
        }
    }
}