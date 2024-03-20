using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints.States
{
    public class ReviewState(Sprint sprint) : SprintState(sprint)
    {
        public override void SetState()
        {
            sprint.ChangeState(new ReviewState(sprint));
        }

        public override void NextState()
        {
            //ToDo: Check if review is uploaded, than change state to closed
            sprint.ChangeState(new ClosedState(sprint));
        }
    }
}
