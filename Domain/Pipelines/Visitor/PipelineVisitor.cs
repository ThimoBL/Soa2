using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines.Actions.AnalyzeAction;
using Domain.Pipelines.Actions.AnalyzeAction.SonarCube;
using Domain.Pipelines.Actions.BuildAction;
using Domain.Pipelines.Actions.DeployAction;
using Domain.Pipelines.Actions.PackageAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Pipelines.Actions.TestAction;
using Domain.Pipelines.Actions.UtilityAction;

namespace Domain.Pipelines.Visitor
{
    public class PipelineVisitor : IPipelineVisitor
    {
        public void Visit(SonarCubeExecutionAction action)
        {
            Console.WriteLine("SonarCubeExecutionAction");
        }

        public void Visit(SonarCubePreparationAction action)
        {
            Console.WriteLine("SonarCubePreparationAction");
        }

        public void Visit(SonarCubeReportingAction action)
        {
            Console.WriteLine("SonarCubeReportingAction");
        }

        public void Visit(AnalyzeCompositeAction action)
        {
            Console.WriteLine("AnalyzeCompositeAction");
        }

        public void Visit(AnalyzeSonarCubeCompositeAction action)
        {
            Console.WriteLine("AnalyzeSonarCubeCompositeAction");
        }

        public void Visit(BuildAntAction action)
        {
            Console.WriteLine("BuildAntAction");
        }

        public void Visit(BuildCompositeAction action)
        {
            Console.WriteLine("BuildCompositeAction");
        }

        public void Visit(BuildDotNetAction action)
        {
            Console.WriteLine("BuildDotNetAction");
        }

        public void Visit(BuildJenkinsAction action)
        {
            Console.WriteLine("BuildJenkinsAction");
        }

        public void Visit(BuildMavenAction action)
        {
            Console.WriteLine("BuildMavenAction");
        }

        public void Visit(DeployAzureAction action)
        {
            Console.WriteLine("DeployAzureAction");
        }

        public void Visit(DeployCompositeAction action)
        {
            Console.WriteLine("DeployCompositeAction");
        }

        public void Visit(DeployGithubAction action)
        {
            Console.WriteLine("DeployGithubAction");
        }

        public void Visit(PackageInstallAction action)
        {
            Console.WriteLine("PackageInstallAction");
        }

        public void Visit(SourceAzureAction action)
        {
            Console.WriteLine("SourceAzureAction");
        }

        public void Visit(SourceCompositeAction action)
        {
            Console.WriteLine("SourceCompositeAction");
        }

        public void Visit(SourceGithubAction action)
        {
            Console.WriteLine("SourceGithubAction");
        }

        public void Visit(TestCompositeAction action)
        {
            Console.WriteLine("TestCompositeAction");
        }

        public void Visit(TestNUnitAction action)
        {
            Console.WriteLine("TestNUnitAction");
        }

        public void Visit(TestSeleniumAction action)
        {
            Console.WriteLine("TestSeleniumAction");
        }

        public void Visit(UtilityAction action)
        {
            Console.WriteLine("UtilityAction");
        }
    }
}
