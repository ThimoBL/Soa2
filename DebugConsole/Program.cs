using Domain.Backlog;
using Domain.Roles;
using Domain.Sprints;

Console.WriteLine("Starting domain...");

var sprintBacklog = new SprintBacklog();
var scrumMaster = new ScrumMaster("John Doe", "JohnDoe@email.nl", "password");

var sprint = new ReleaseSprint(sprintBacklog, scrumMaster);