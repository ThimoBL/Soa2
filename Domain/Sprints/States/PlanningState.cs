using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints.States
{
    public class PlanningState(Sprint sprint) : SprintState
    {
        public override void SetState()
        {
            sprint.ChangeState(new PlanningState(sprint));
        }

        public override void NextState()
        {
            sprint.ChangeState(new ActiveState(sprint));
        }
    }
}
