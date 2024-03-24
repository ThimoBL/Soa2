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
    public class ForumTests
    {
        [Fact]
        public void Start_Discussion_On_Forum_With_Thread()
        {
            //Arrange
            Constants.ProjectExample.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, Constants.PipelineExample, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = Constants.ProjectExample.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);
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
            Constants.ProjectExample.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, Constants.PipelineExample, new GitStrategy(),
                SprintType.Release);

            var releaseSprint = Constants.ProjectExample.Sprints.First();

            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, releaseSprint);
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
    }
}