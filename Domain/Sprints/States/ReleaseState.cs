using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints.States
{
    public class ReleaseState(Sprint sprint) : SprintState(sprint)
    {
        //ToDo: implement run pipeline method

        //Todo: implement cancel release method
        public override void SetState()
        {
            sprint.ChangeState(new ReleaseState(sprint));
        }

        public override void NextState()
        {
            sprint.ChangeState(new ClosedState(sprint));
            //Todo: Check if pipeline is successful
        }
    }
}
