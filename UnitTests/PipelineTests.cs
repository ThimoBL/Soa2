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
            var pipeline = new Pipeline("Pipeline 1");

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
    }
}