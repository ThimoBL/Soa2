using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlog;
using Domain.Roles;
using Domain.Sprints.States;

namespace Domain.Sprints
{
    public abstract class Sprint
    {
        protected Sprint(SprintBacklog sprintBacklog, ScrumMaster scrumMaster)
        {
            SprintBacklog = sprintBacklog;
            ScrumMaster = scrumMaster;
            SprintState = new OpenState(this);
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SprintBacklog SprintBacklog { get; set; }
        public ScrumMaster ScrumMaster { get; set; }
        public SprintState SprintState { get; set; }

        public void ChangeState(SprintState sprintState)
        {
            SprintState = sprintState;
        }

        public abstract void Accept(ISprintVisitor visitor);
    }
}