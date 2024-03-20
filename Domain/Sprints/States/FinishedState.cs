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
            var visitor = new SprintVisitor(sprint);
            sprint.Accept(visitor);
        }
    }
}
