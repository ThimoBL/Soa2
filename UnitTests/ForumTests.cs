using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlogs;
using Domain.Backlogs.States;
using Domain.Forums;
using Domain.GeneralModels;
using Domain.Notifications;
using Domain.Pipelines;
using Domain.Sprints.Factory;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Domain.VersionControl.Strategy;

namespace UnitTests
{
    public class ForumTests
    {
        [Fact]
        public void Start_Discussion_On_Forum_With_Thread()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release, new NotificationService());

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint,
                    new NotificationService());
            var thread = new Threads("Test Thread", "Test Description", backlogItem);
            var forum = new Forum();

            //Act
            forum.AddThread(thread);

            //Assert
            Assert.Contains(thread, forum.Threads);
            Assert.Equal(1, forum.Threads.Count);
        }

        [Fact]
        public void React_On_Existing_Thread()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release, new NotificationService());

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint,
                    new NotificationService());
            var thread = new Threads("Test Thread", "Test Description", backlogItem);
            var forum = new Forum();
            forum.AddThread(thread);

            var message = new Message("Test Message", Constants.ExampleDeveloper);
            var reaction = new Message("Reaction message", Constants.ExampleTester);

            //Act
            thread.AddMessage(message);
            thread.AddMessage(reaction);

            //Assert
            Assert.Contains(message, thread.Messages);
            Assert.Contains(reaction, thread.Messages);
            Assert.Equal(2, thread.Messages.Count);
        }

        [Fact]
        public void Cant_Post_Message_On_Closed_Thread()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release, new NotificationService());

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint,
                    new NotificationService());

            backlogItem.ChangeState(new DoneState(backlogItem));

            var thread = new Threads("Test Thread", "Test Description", backlogItem);
            var forum = new Forum();

            forum.AddThread(thread);

            var message = new Message("Test Message", Constants.ExampleDeveloper);

            //Act & Assert
            Assert.Throws<InvalidOperationException>(() => thread.AddMessage(message));
        }
    }
}