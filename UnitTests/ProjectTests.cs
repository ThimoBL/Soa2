using Domain;
using Domain.Roles;

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
            var project = new Project(expectedTitle, expectedDescription, expectedOwner);

            //Assert
            Assert.NotNull(project);
        }
    }
}