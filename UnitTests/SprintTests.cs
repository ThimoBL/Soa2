using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlog;
using Domain.Roles;
using Domain.Sprints;
using Domain.Sprints.States;
using Moq;

namespace UnitTests
{
    public class SprintTests
    {
        private SprintBacklog _sprintBacklog;
        private ScrumMaster _scrumMaster;

        public SprintTests()
        {
            _sprintBacklog = new SprintBacklog();
            _scrumMaster = new ScrumMaster("John Doe", "Johndoe@email.nl", "password");
        }

        [Fact]
        public void ReleaseSprint_Closed_After_FinishedState()
        {
            // Arrange
            var releaseSprint = new ReleaseSprint(_sprintBacklog, _scrumMaster);
            var visitor = new SprintVisitor(releaseSprint);

            // Act
            releaseSprint.Accept(visitor);

            // Assert
            // Here you would assert that the state of releaseSprint has changed as expected.
            // This might require exposing some state through properties or using reflection if the state change is internal.
            // For the purpose of this example, let's assume we're checking a 'State' property that indicates the sprint's current state.
            Assert.IsType<ClosedState>(releaseSprint.SprintState); // xUnit uses Assert.IsType<> instead of Assert.IsInstanceOf<>

        }
    }
}
