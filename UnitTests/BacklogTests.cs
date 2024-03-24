using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlogs;
using Domain.Backlogs.States;
using Domain.Forums;
using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Roles;
using Domain.Sprints.Factory;
using Domain.VersionControl;
using Domain.VersionControl.Factory;

namespace UnitTests
{
    public class BacklogTests
    {
        [Fact]
        public void User_Can_Create_Backlog_Item()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = project.Sprints.First();
            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);


            //Act
            releaseSprint.AddBacklogItem(backlogItem);

            //Assert
            Assert.NotNull(backlogItem);
            Assert.Equal("ExampleTitle", backlogItem.Title);
            Assert.Equal("ExampleDescription", backlogItem.Description);
            Assert.Equal(3, backlogItem.StoryPoints);
            Assert.Equal(Constants.ExampleDeveloper, backlogItem.Developer);
        }

        [Fact]
        public void User_Can_Create_Tasks_For_Backlog_Item()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release);

            var otherDev = new Developer("Other Developer", "otherDev@email.nl", "password");

            var releaseSprint = project.Sprints.First();
            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);
            var task = new Domain.Backlogs.Task("ExampleTask", "ExampleDescription", otherDev,
                releaseSprint);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            backlogItem.AddTask(task);

            //Assert
            Assert.NotNull(backlogItem.Tasks);
            Assert.Equal("ExampleTask", backlogItem.Tasks.First().Title);
            Assert.Equal("ExampleDescription", backlogItem.Tasks.First().Description);
            Assert.Equal(otherDev, backlogItem.Tasks.First().Developer);
        }

        [Fact]
        public void Backlog_Item_Can_Have_Message()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);
            var thread = new Threads("ExampleTitle", "ExampleDescription", backlogItem);
            var message = new Message("ExampleMessage", Constants.ExampleDeveloper);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            thread.AddMessage(message);

            //Assert
            Assert.NotNull(message);
            Assert.Equal("ExampleMessage", thread.Messages.First().Content);
            Assert.Equal(Constants.ExampleDeveloper, thread.Messages.First().Author);
        }

        [Fact]
        public void Default_BacklogItemStatus_Is_ToDo()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            var status = backlogItem.Status;

            //Assert
            Assert.Equal(typeof(ToDoState), status.GetType());
        }

        [Fact]
        public void BacklogItem_Can_Be_Moved_To_InProgress()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            backlogItem.NextState();

            //Assert
            Assert.Equal(typeof(DoingState), backlogItem.Status.GetType());
        }

        [Fact]
        public void BacklogItem_Can_Be_Moved_To_Done()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            backlogItem.NextState();
            backlogItem.NextState();

            //Assert
            Assert.Equal(typeof(DoneState), backlogItem.Status.GetType());
        }

        [Fact]
        public void BacklogItem_Can_Be_Moved_To_ReadyForTesting()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            backlogItem.NextState();
            backlogItem.NextState();
            backlogItem.NextState();

            //Assert
            Assert.Equal(typeof(ReadyForTestingState), backlogItem.Status.GetType());
        }

        [Fact]
        public void BacklogItem_Can_Be_Moved_To_Testing()
        {
                        //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                               Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                                              new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                               Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                                              SprintType.Release);

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            backlogItem.NextState();
            backlogItem.NextState();
            backlogItem.NextState();
            backlogItem.NextState();

            //Assert
            Assert.Equal(typeof(TestingState), backlogItem.Status.GetType());
        }

        [Fact]
        public void BacklogItem_Can_Be_Moved_To_Tested()
        {
                        //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                                              Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                                                                                           new VersionControlFactory());

            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                                              Constants.ExampleScrumMaster, Constants.ExampleTester, pipeline, new GitStrategy(),
                                                                                           SprintType.Release);

            var releaseSprint = project.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);

            releaseSprint.AddBacklogItem(backlogItem);

            //Act
            backlogItem.NextState();
            backlogItem.NextState();
            backlogItem.NextState();
            backlogItem.NextState();
            backlogItem.NextState();

            //Assert
            Assert.Equal(typeof(TestedState), backlogItem.Status.GetType());
        }
    }
}