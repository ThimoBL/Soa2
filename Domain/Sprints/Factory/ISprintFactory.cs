using Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints.Factory
{
    public interface ISprintFactory
    {
        Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster, Pipeline pipeline,
            SprintType sprintType, IGitStrategy gItStrategy);
    }
}
