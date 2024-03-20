using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints.States
{
    public class ClosedState(Sprint sprint) : SprintState(sprint)
    {
        public override void SetState()
        {
            sprint.ChangeState(new ClosedState(sprint));
        }

        public override void NextState()
        {
            throw new NotImplementedException();
        }
    }
}
