using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints.States
{
    public class FinishedState(Sprint sprint) : SprintState(sprint)
    {
        public override void SetState()
        {
            sprint.ChangeState(new FinishedState(sprint));
        }

        public override void NextState()
        {
            //ToDo: Check if sprint is release or review with behavioural/constructial pattern
            sprint.ChangeState(new ReleaseSprint(sprint));
        }
    }
}
