using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlog;
using Domain.Roles;

namespace Domain.Sprints
{
    public class ReviewSprint : Sprint
    {
        //ToDo: add reviews to sprint

        //ToDo: add optional pipeline to sprint (maybe for review sprint but is an assumption)
        public ReviewSprint(SprintBacklog sprintBacklog, ScrumMaster scrumMaster) : base(sprintBacklog, scrumMaster)
        {
        }
    }
}