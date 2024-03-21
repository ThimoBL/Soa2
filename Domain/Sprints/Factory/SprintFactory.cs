using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;

namespace Domain.Sprints.Factory
{
    public class SprintFactory() : ISprintFactory
    {
        public Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            SprintType sprintType) =>
            sprintType switch
            {
                SprintType.Release => new ReleaseSprint(title, startDate, endDate, scrumMaster),
                SprintType.Review => new ReviewSprint(title, startDate, endDate, scrumMaster),
                _ => throw new ArgumentException("Invalid sprint type")
            };
    }
}