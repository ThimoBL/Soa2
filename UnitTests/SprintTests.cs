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

namespace UnitTests
{
    public class SprintTests
    {
        [Fact]
        public void ReleaseSprint_Closed_After_FinishedState()
        {
            // Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(), new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                Constants.ExampleTester, pipeline, new GitStrategy(), SprintType.Release);

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

            //Analyze action
            var compositeAnalyzeAction = new AnalyzeSonarCubeCompositeAction();
            var sonarCubePreparation = new SonarCubePreparationAction();
            var sonarCubeReporting = new SonarCubeReportingAction();
            var sonarCubeExecution = new SonarCubeExecutionAction();

            compositeAnalyzeAction.AddAction(sonarCubePreparation);
            compositeAnalyzeAction.AddAction(sonarCubeReporting);
            compositeAnalyzeAction.AddAction(sonarCubeExecution);

            //Build action
            var compositeBuildAction = new BuildCompositeAction();
            var buildMavenAction = new BuildMavenAction();
            var buildDotNetAction = new BuildDotNetAction();
            var buildJenkinsAction = new BuildJenkinsAction();
            var buildAntAction = new BuildAntAction();

            compositeBuildAction.AddAction(buildMavenAction);
            compositeBuildAction.AddAction(buildDotNetAction);
            compositeBuildAction.AddAction(buildJenkinsAction);
            compositeBuildAction.AddAction(buildAntAction);

            //Deploy action
            var compositeDeployAction = new DeployCompositeAction();
            var deployAzureAction = new DeployAzureAction();
            var deployGithubAction = new DeployGithubAction();

            compositeDeployAction.AddAction(deployAzureAction);
            compositeDeployAction.AddAction(deployGithubAction);

            //Source action
            var compositeSourceAction = new SourceCompositeAction();
            var sourceGithubAction = new SourceGithubAction();
            var sourceAzureAction = new SourceAzureAction();

            compositeSourceAction.AddAction(sourceGithubAction);
            compositeSourceAction.AddAction(sourceAzureAction);

            //Test action
            var compositeTestAction = new TestCompositeAction();
            var testNUnitAction = new TestNUnitAction();
            var testSeleniumAction = new TestSeleniumAction();

            compositeTestAction.AddAction(testNUnitAction);
            compositeTestAction.AddAction(testSeleniumAction);

            mockPipeline.Object.AddAction(compositeSourceAction);
            mockPipeline.Object.AddAction(new PackageInstallAction());
            mockPipeline.Object.AddAction(compositeBuildAction);
            mockPipeline.Object.AddAction(compositeTestAction);
            mockPipeline.Object.AddAction(compositeAnalyzeAction);
            mockPipeline.Object.AddAction(compositeDeployAction);
            mockPipeline.Object.AddAction(new UtilityAction());

            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(), new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                tester, mockPipeline.Object, new GitStrategy(), SprintType.Release);

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