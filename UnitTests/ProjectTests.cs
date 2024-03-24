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
    }
}