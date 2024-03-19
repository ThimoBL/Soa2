using Domain.Backlog;
using Domain.Roles;
using DomainService;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void ProjectCanBeCreated()
        {
            //Arrange
            string expectedTitle = "Project Alpha";
            string expectedDescription = "This is a test project";
            var expectedOwner = new ProductOwner("Name", "Email", "Password");
            var expectedBacklog = new ProductBacklog();

            //Act
            var project = new Project(expectedTitle, expectedDescription, expectedOwner, expectedBacklog);

            //Assert
            Assert.NotNull(project);
        }
    }
}