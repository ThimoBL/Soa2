using Domain.Backlogs;
using Domain.Forums;
using Domain.GeneralModels;
using Domain.Notifications;
using Domain.Pipelines;
using Domain.Pipelines.Actions.AnalyzeAction.SonarCube;
using Domain.Pipelines.Actions.AnalyzeAction;
using Domain.Pipelines.Actions.BuildAction;
using Domain.Pipelines.Actions.DeployAction;
using Domain.Pipelines.Actions.PackageAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Pipelines.Actions.TestAction;
using Domain.Pipelines.Actions.UtilityAction;
using Domain.Roles;
using Domain.Sprints.Factory;
using Domain.Sprints.States;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Domain.VersionControl.Strategy;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Starting domain...");

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<ISprintFactory, SprintFactory>();
serviceCollection.AddSingleton<IVersionControlFactory, VersionControlFactory>();
var serviceProvider = serviceCollection.BuildServiceProvider();

var sprintFactory = serviceProvider.GetRequiredService<ISprintFactory>();
var versionControlFactory = serviceProvider.GetRequiredService<IVersionControlFactory>();

var productOwner = new ProductOwner("Name", "Email", "Password");
var project = new Project("Project Alpha", "This is a test project", productOwner, VersionControlTypes.Git,
    sprintFactory, versionControlFactory);
var scrumMaster = new ScrumMaster("John Doe", "JohnDoe@email.nl", "password");
var tester = new Tester("John Doe", "JohnDoe@email.nl", "password");

var versionControl = project.GetGitStrategy();

productOwner.AddPreferences(new MailPublisher());
productOwner.AddPreferences(new SlackPublisher());

productOwner.Notify();

var pipeline = new Pipeline("Pipeline 1");

// Add actions to pipeline
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

var sprint = sprintFactory.CreateSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14),
    scrumMaster, tester, pipeline, new GitStrategy(), project, SprintType.Release, new NotificationService());

sprint.ChangeState(new FinishedState(sprint));
sprint.NextSprintState();

versionControl.Commit("Commit message");

var developer = new Developer("John Doe", "JohnDoe@email.nl", "password");
// var backlogItem = new BacklogItem("Backlog item 1", "Description", 1, developer);
// var thread = new Threads("Thread 1", "Description", sprint);
// var message = new Message("This is a backlog item message", developer);

// thread.AddMessage(message);
// backlogItem.AddThread(thread);
// sprint.AddBacklogItem(backlogItem);
// sprint.GetBacklogItems().First().Thread.ReadAllMessages();
sprint.RunPipeline();