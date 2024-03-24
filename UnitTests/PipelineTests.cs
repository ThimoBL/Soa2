using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Pipelines.Actions.AnalyzeAction;
using Domain.Pipelines.Actions.AnalyzeAction.SonarCube;
using Domain.Pipelines.Actions.BuildAction;
using Domain.Pipelines.Actions.DeployAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Pipelines.Actions.TestAction;
using Domain.Pipelines.Visitor;
using Domain.Sprints.Factory;
using Domain.VersionControl.Factory;
using Domain.VersionControl;
using Moq;
using Domain.Sprints;

namespace UnitTests
{
    public class PipelineTests
    {
        [Fact]
        public void User_Can_Setup_Pipeline()
        {
            //Arrange
            var mockPipeline = new Mock<Pipeline>("Pipeline 1");

            mockPipeline.Object.AddAction(Constants.SourceAction);

            var visitor = new Mock<IPipelineVisitor>();

            //Act
            visitor.Object.Visit(Constants.SourceAction);

            //Assert
            visitor.Verify(x => x.Visit(Constants.SourceAction), Times.Once);
        }

        [Fact]
        public void User_Can_Setup_Nested_Actions_In_Pipeline()
        {
            //Arrange
            var mockPipeline = new Mock<Pipeline>("Pipeline 1");
            var compositeAnalyzeAction = new AnalyzeSonarCubeCompositeAction();
            var compositePackageAction = new SonarCubePreparationAction();

            compositeAnalyzeAction.AddAction(compositePackageAction);
            mockPipeline.Object.AddAction(compositeAnalyzeAction);

            var visitor = new Mock<IPipelineVisitor>();

            //Act
            visitor.Object.Visit(compositePackageAction);

            //Assert
            visitor.Verify(x => x.Visit(compositePackageAction), Times.Once);
        }

        [Fact]
        public void Run_Pipeline_Manually_From_Sprint()
        {
            //Arrange
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
            mockPipeline.Object.AddAction(Constants.PackageAction);
            mockPipeline.Object.AddAction(compositeBuildAction);
            mockPipeline.Object.AddAction(compositeTestAction);
            mockPipeline.Object.AddAction(compositeAnalyzeAction);
            mockPipeline.Object.AddAction(compositeDeployAction);
            mockPipeline.Object.AddAction(Constants.UtilityAction);

            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            var mockSprint = new Mock<ReleaseSprint>("Sprint 1", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, mockPipeline.Object, new GitStrategy(),
                project);

            //Act
            mockSprint.Object.RunPipeline();

            //Assert
            mockSprint.Verify(x => x.RunPipeline(), Times.AtLeastOnce);
        }
    }
}