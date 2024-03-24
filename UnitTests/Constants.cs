using System;
using Domain.Backlogs;
using Domain.VersionControl.Factory;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Pipelines.Actions.AnalyzeAction.SonarCube;
using Domain.Pipelines.Actions.BuildAction;
using Domain.Pipelines.Actions.DeployAction;
using Domain.Pipelines.Actions.PackageAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Pipelines.Actions.TestAction;
using Domain.Pipelines.Actions.UtilityAction;
using Domain.Roles;
using Domain.Sprints.Factory;
using Domain.VersionControl.Factory;

namespace UnitTests
{
    public static class Constants
    {
        //Roles
        public static ProductOwner ExampleProductOwner = new("Name", "Email", "Password");
        public static ScrumMaster ExampleScrumMaster = new("John Doe", "Johndoe@email.nl", "password");
        public static Developer ExampleDeveloper = new("Developer", "Email", "Password");
        public static Tester ExampleTester = new("Tester", "Email", "Password");

        //Project
        public static Project ProjectExample = new("Project Alpha", "This is a test project",
            ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(), new VersionControlFactory());

        //Pipeline
        public static Pipeline PipelineExample = new("Pipeline Alpha");

        //Pipeline Actions
        public static SourceGithubAction SourceAction = new();
        public static PackageInstallAction PackageAction = new();
        public static BuildDotNetAction BuildAction = new();
        public static TestNUnitAction TestAction = new();
        public static SonarCubeExecutionAction AnalyseAction = new();
        public static DeployGithubAction DeployAction = new();
        public static UtilityAction UtilityAction = new();

    }
}