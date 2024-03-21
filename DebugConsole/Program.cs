using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.States;

Console.WriteLine("Starting domain...");

var scrumMaster = new ScrumMaster("John Doe", "JohnDoe@email.nl", "password");

var sprint = new ReleaseSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), scrumMaster);
sprint.NextSprintState();