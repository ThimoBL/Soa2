using Domain.GeneralModels;
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


var sprint = sprintFactory.CreateSprint("Sprint 1", DateTime.Now, DateTime.Now.AddDays(14), scrumMaster, SprintType.Release);
sprint.NextSprintState();