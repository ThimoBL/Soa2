using Domain;
using Domain.GeneralModels;
using Domain.Roles;
using Domain.Sprints.Factory;
using Domain.VersionControl.Factory;
using Moq;

namespace UnitTests
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectCanBeCreated()
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
        }
    }
}