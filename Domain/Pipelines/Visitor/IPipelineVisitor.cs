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
    public interface IPipelineVisitor
    {
        void Visit(SonarCubeExecutionAction action);
        void Visit(SonarCubePreparationAction action);
        void Visit(SonarCubeReportingAction action);
        void Visit(AnalyzeCompositeAction action);
        void Visit(AnalyzeSonarCubeCompositeAction action);
        void Visit(BuildAntAction action);
        void Visit(BuildCompositeAction action);
        void Visit(BuildDotNetAction action);
        void Visit(BuildJenkinsAction action);
        void Visit(BuildMavenAction action);
        void Visit(DeployAzureAction action);
        void Visit(DeployCompositeAction action);
        void Visit(DeployGithubAction action);
        void Visit(PackageInstallAction action);
        void Visit(SourceAzureAction action);
        void Visit(SourceCompositeAction action);
        void Visit(SourceGithubAction action);
        void Visit(TestCompositeAction action);
        void Visit(TestNUnitAction action);
        void Visit(TestSeleniumAction action);
        void Visit(UtilityAction action);
    }
}
