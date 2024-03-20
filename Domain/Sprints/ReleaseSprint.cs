using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlog;
using Domain.Roles;

namespace Domain.Sprints
{
    public class ReleaseSprint : Sprint
    {
        //ToDo: add pipeline to sprint
        public ReleaseSprint(SprintBacklog sprintBacklog, ScrumMaster scrumMaster) : base(sprintBacklog, scrumMaster)
        {
        }
    }
}