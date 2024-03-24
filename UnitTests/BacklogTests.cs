using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlogs;
using Domain.Forums;
using Domain.GeneralModels;
using Domain.Pipelines;
using Domain.Sprints.Factory;
using Domain.VersionControl;
using Domain.VersionControl.Factory;

namespace UnitTests
{
    public class BacklogTests
    {
        [Fact]
        public void Backlog_Item_Can_Have_Message()
        {
            //Arrange
            var pipeline = new Pipeline("Pipeline 1");
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(), new VersionControlFactory());

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
    }
}