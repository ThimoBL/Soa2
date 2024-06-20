using Domain;
using Domain.Backlogs;
using Domain.GeneralModels;
using Domain.Notifications;
using Domain.Pipelines;
using Domain.Roles;
using Domain.Sprints.Factory;
using Domain.VersionControl;
using Domain.VersionControl.Factory;
using Domain.VersionControl.Strategy;
using Moq;

namespace UnitTests
{
    public class ProjectTests
    {
        [Fact]
        public void Project_Should_Be_Created_By_ProductOwner()
        {
            //Arrange
            string expectedTitle = "Project Alpha";
            string expectedDescription = "This is a test project";
            var expectedOwner = new ProductOwner("Name", "Email", "Password");

            //Act
            var project = new Project(expectedTitle, expectedDescription, expectedOwner, VersionControlTypes.Git,
                new SprintFactory(), new VersionControlFactory());

            //Assert
            Assert.NotNull(project);
            Assert.Equal(expectedTitle, project.Title);
            Assert.Equal(expectedDescription, project.Description);
            Assert.Equal(expectedOwner, project.Owner);
        }

        [Fact]
        public void Project_Can_Have_BackogItems()
        {
            //Arrange
            var project = new Project("Project Alpha", "This is a test project",
                Constants.ExampleProductOwner, VersionControlTypes.Git, new SprintFactory(),
                new VersionControlFactory());
            project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7),
                Constants.ExampleScrumMaster, Constants.ExampleTester, new Pipeline("Pipeline 1"), new GitStrategy(),
                SprintType.Release, new NotificationService());

            var sprint = project.Sprints.First();
            var backlogItem =
                new BacklogItem("ExampleTitle", "ExampleDescription", 3, Constants.ExampleDeveloper, sprint,
                    new NotificationService());

            //Act
            project.AddBacklog(backlogItem);

            //Assert
            Assert.Contains(backlogItem, project.ProductBacklog);
            Assert.Equal(1, project.ProductBacklog.Count);
        }
    }
}