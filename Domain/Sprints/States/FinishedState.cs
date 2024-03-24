using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Sprints.Visitor;

namespace Domain.Sprints.States
{
    public class FinishedState : SprintState
    {
        private readonly Sprint _sprint;

        public FinishedState(Sprint sprint)
        {
            _sprint = sprint;

            if (sprint.GetType() == typeof(ReleaseSprint))
            {
                _sprint.RunPipeline();
            }
        }

        public override void SetState()
        {
            _sprint.ChangeState(new FinishedState(_sprint));
        }

        public override void NextState()
        {
            var visitor = new SprintVisitor(_sprint);
            _sprint.AcceptSprint(visitor);

            if (_sprint.GetType() == typeof(ReviewSprint) && _sprint.IsReviewUploaded())
            {
                _sprint.ChangeState(new ClosedState(_sprint));
            }
        }
    }
}
