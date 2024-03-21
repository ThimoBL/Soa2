using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;

namespace Domain.Sprints.Factory
{
    public class SprintFactory
    {
        public Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            SprintType sprintType)
        {
            switch (sprintType)
            {
                case SprintType.Release:
                    return new ReleaseSprint(title, startDate, endDate, scrumMaster);
                case SprintType.Review:
                    return new ReviewSprint(title, startDate, endDate, scrumMaster);
                default:
                    throw new ArgumentException("Invalid sprint type");
            }
        }
    }
}