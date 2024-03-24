using System;
using Domain.Backlogs;
using Domain.VersionControl.Factory;
using System.Collections.Generic;
using System.Linq;
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

namespace UnitTests
{
    public static class Constants
    {
        //Roles
        public static readonly ProductOwner ExampleProductOwner = new("Name", "Email", "Password");
        public static readonly ScrumMaster ExampleScrumMaster = new("John Doe", "Johndoe@email.nl", "password");
        public static readonly Developer ExampleDeveloper = new("Developer", "Email", "Password");
        public static readonly Tester ExampleTester = new("Tester", "Email", "Password");

        //Project
        public static readonly Project ProjectExample = new("Project Alpha", "This is a test project",
            ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(), new VersionControlFactory());

        //Pipeline
        public static readonly Pipeline PipelineExample = new("Pipeline Alpha");

        //Pipeline Actions
        public static readonly SourceGithubAction SourceAction = new();
        public static readonly PackageInstallAction PackageAction = new();
        public static readonly BuildDotNetAction BuildAction = new();
        public static readonly TestNUnitAction TestAction = new();
        public static readonly SonarCubeExecutionAction AnalyzeAction = new();
        public static readonly DeployGithubAction DeployAction = new();
        public static readonly UtilityAction UtilityAction = new();

    }
}