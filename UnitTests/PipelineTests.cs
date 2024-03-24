using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines;
using Domain.Pipelines.Actions.AnalyzeAction;
using Domain.Pipelines.Actions.AnalyzeAction.SonarCube;
using Domain.Pipelines.Visitor;
using Moq;

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
    }
}
