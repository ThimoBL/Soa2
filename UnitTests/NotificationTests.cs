using Domain.GeneralModels;
using Domain.Notifications;
using Domain.Pipelines;
using Domain.Sprints.Factory;
using Domain.VersionControl.Factory;
using Domain.VersionControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlogs;
using Domain.Backlogs.States;
using Domain.Notifications.Interfaces;
using Domain.Roles;
using Moq;
using Domain.Sprints;

namespace UnitTests
{
    public class NotificationTests
    {
        [Fact]
        public void Notify_Tester_When_BacklogItem_Is_ReadyForTesting()
        {
            //Arrange
            var mockTester = new Mock<Tester>("Tester", "Tester@email.nl", "password");
            var pipeline = new Pipeline("Pipeline 1");
            mockTester.Object.AddPreferences(new MailPublisher());
            mockTester.Object.AddPreferences(new SlackPublisher());

            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), Constants.ExampleScrumMaster,
                               mockTester.Object, pipeline, new GitStrategy(), SprintType.Release, new NotificationService());

            var sprint = project.Sprints.First();

            var backlogItem = new Mock<BacklogItem>("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper,
                sprint, new NotificationService());

            //Act
            backlogItem.Object.ChangeState(new ReadyForTestingState(backlogItem.Object));

            //Assert
            mockTester.Verify(x => x.Notify(), Times.AtLeastOnce);
        }
    }
}