using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines;
using Domain.Roles;

namespace Domain.Sprints.Factory
{
    public class SprintFactory() : ISprintFactory
    {
        public Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Pipeline pipeline, SprintType sprintType) =>
            sprintType switch
            {
                SprintType.Release => new ReleaseSprint(title, startDate, endDate, scrumMaster, pipeline),
                SprintType.Review => new ReviewSprint(title, startDate, endDate, scrumMaster, pipeline),
                _ => throw new ArgumentException("Invalid sprint type")
            };
    }
}