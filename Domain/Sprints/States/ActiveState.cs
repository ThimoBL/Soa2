using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints.States
{
    public class ActiveState(Sprint sprint) : SprintState(sprint)
    {
        public override void SetState()
        {
            sprint.ChangeState(new ActiveState(sprint));
        }

        public override void NextState()
        {
            sprint.ChangeState(new FinishedState(sprint));
        }
    }
}
