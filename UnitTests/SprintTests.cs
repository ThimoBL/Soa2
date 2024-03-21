using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.States;
using Moq;

namespace UnitTests
{
    public class SprintTests
    {
        private readonly ScrumMaster _scrumMaster = new("John Doe", "Johndoe@email.nl", "password");

        [Fact]
        public void ReleaseSprint_Closed_After_FinishedState()
        {
            // Arrange
            var releaseSprint = new ReleaseSprint("John Doe", DateTime.Now, DateTime.Now.AddDays(7), _scrumMaster);
            releaseSprint.ChangeState(new FinishedState(releaseSprint));

            // Act
            releaseSprint.NextSprintState();

            // Assert
            Assert.IsType<ClosedState>(releaseSprint.SprintState);
        }
    }
}