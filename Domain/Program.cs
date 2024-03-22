using Domain.GeneralModels;
using Domain.Notifications;
using Domain.Pipelines;
using Domain.Pipelines.Actions.AnalyzeAction;
using Domain.Pipelines.Actions.AnalyzeAction.SonarCube;
using Domain.Pipelines.Actions.BuildAction;
using Domain.Pipelines.Actions.PackageAction;
using Domain.Pipelines.Actions.SourceAction;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Starting domain...");

var serviceCollection = new ServiceCollection();
serviceCollection.AddSingleton<ISprintFactory, SprintFactory>();
var serviceProvider = serviceCollection.BuildServiceProvider();

var sprintFactory = serviceProvider.GetRequiredService<ISprintFactory>();

var productOwner = new ProductOwner("Name", "Email", "Password");
var project = new Project("Project Alpha", "This is a test project", productOwner, sprintFactory);
var scrumMaster = new ScrumMaster("John Doe", "JohnDoe@email.nl", "password");

productOwner.AddPreferences(new MailPublisher());
productOwner.AddPreferences(new SlackPublisher());

productOwner.Notify();

var pipeline = new Pipeline("Pipeline 1");

// Add actions to pipeline
var sourceAction = new SourceCompositeAction();
sourceAction.AddAction(new SourceGithubAction());

var packageAction = new PackageInstallAction();

var buildAction = new BuildCompositeAction();
buildAction.AddAction(new BuildMavenAction());

pipeline.AddAction(sourceAction);
pipeline.AddAction(packageAction);
pipeline.AddAction(buildAction);

var sprint = sprintFactory.CreateSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), scrumMaster, pipeline, SprintType.Release);
sprint.NextSprintState();
sprint.RunPipeline();