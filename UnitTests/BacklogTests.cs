using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlogs;
using Domain.Forums;
using Domain.Sprints.Factory;
using Domain.VersionControl;

namespace UnitTests
{
    public class BacklogTests
    {
        [Fact]
        public void Backlog_Item_Can_Have_Message()
        {
            //Arrange
            Constants.ProjectExample.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, Constants.PipelineExample, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = Constants.ProjectExample.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);
            var thread = new Threads("ExampleTitle", "ExampleDescription");
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