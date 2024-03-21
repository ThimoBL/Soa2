using Domain.GeneralModels;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.Factory;
using Domain.Sprints.States;
using Moq;

namespace UnitTests
{
    public class SprintTests
    {
        private readonly Project _project = new("Project Alpha", "This is a test project",
            new ProductOwner("Name", "Email", "Password"), new SprintFactory());

        private readonly ScrumMaster _scrumMaster = new("John Doe", "Johndoe@email.nl", "password");


        [Fact]
        public void ReleaseSprint_Closed_After_FinishedState()
        {
            // Arrange
            _project.CreateSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), _scrumMaster, SprintType.Release);
            var releaseSprint = _project.Sprints.First();
            releaseSprint.ChangeState(new FinishedState(releaseSprint));

            // Act
            releaseSprint.NextSprintState();

            // Assert
            Assert.IsType<ClosedState>(releaseSprint.SprintState);
        }
    }
}