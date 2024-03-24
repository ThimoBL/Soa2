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
using Domain.Pipelines.Actions.PackageAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Pipelines.Actions.TestAction;
using Domain.Pipelines.Visitor;
using Domain.Sprints.Factory;
using Domain.VersionControl.Factory;
using Domain.VersionControl;
using Moq;
using Domain.Sprints;
using Domain.Pipelines.Actions.UtilityAction;

namespace UnitTests
{
    public class PipelineTests
    {
        [Fact]
        public void User_Can_Setup_Pipeline()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");

            pipeline.AddAction(Constants.SourceAction);

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
            var pipeline = new Pipeline("Pipeline 1");
            var compositeAnalyzeAction = new AnalyzeSonarCubeCompositeAction();
            var compositePackageAction = new SonarCubePreparationAction();

            compositeAnalyzeAction.AddAction(compositePackageAction);
            pipeline.AddAction(compositeAnalyzeAction);

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
            var pipeline = new Pipeline("Pipeline 1");

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

            pipeline.AddAction(compositeSourceAction);
            pipeline.AddAction(new PackageInstallAction());
            pipeline.AddAction(compositeBuildAction);
            pipeline.AddAction(compositeTestAction);
            pipeline.AddAction(compositeAnalyzeAction);
            pipeline.AddAction(compositeDeployAction);
            pipeline.AddAction(new UtilityAction());

            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            var mockSprint = new Mock<ReleaseSprint>("Sprint 1", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                project);

            //Act
            mockSprint.Object.RunPipeline();

            //Assert
            mockSprint.Verify(x => x.RunPipeline(), Times.AtLeastOnce);
        }

        [Fact]
        public void Can_Run_Pipeline_With_Full_Actions()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var visitor = new Mock<IPipelineVisitor>();

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

            pipeline.AddAction(compositeSourceAction);
            pipeline.AddAction(new PackageInstallAction());
            pipeline.AddAction(compositeBuildAction);
            pipeline.AddAction(compositeTestAction);
            pipeline.AddAction(compositeAnalyzeAction);
            pipeline.AddAction(compositeDeployAction);
            pipeline.AddAction(new UtilityAction());

            //Act
            visitor.Object.Visit(compositeSourceAction);
            visitor.Object.Visit(new PackageInstallAction());
            visitor.Object.Visit(compositeBuildAction);
            visitor.Object.Visit(compositeTestAction);
            visitor.Object.Visit(compositeAnalyzeAction);
            visitor.Object.Visit(compositeDeployAction);
            visitor.Object.Visit(new UtilityAction());

            //Assert
            visitor.Verify(x => x.Visit(compositeSourceAction), Times.AtLeastOnce);
            visitor.Verify(x => x.Visit(It.IsAny<PackageInstallAction>()), Times.AtLeastOnce);
            visitor.Verify(x => x.Visit(compositeBuildAction), Times.AtLeastOnce);
            visitor.Verify(x => x.Visit(compositeTestAction), Times.AtLeastOnce);
            visitor.Verify(x => x.Visit(compositeAnalyzeAction), Times.AtLeastOnce);
            visitor.Verify(x => x.Visit(compositeDeployAction), Times.AtLeastOnce);
            visitor.Verify(x => x.Visit(It.IsAny<UtilityAction>()), Times.AtLeastOnce);
        }
    }
}