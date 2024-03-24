using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backlogs.States
{
    public class ReadyForTestingState : ItemState
    {
        public ReadyForTestingState(Item item) : base(item)
        {
            item.GetSprint().NotifyTester($"Item {item.Title} is ready for testing");
        }

        public override void SetState()
        {
            item.ChangeState(new ReadyForTestingState(item));
        }

        public override void NextState()
        {
            item.ChangeState(new TestingState(item));
        }

        public override void PreviousState()
        {
            item.ChangeState(new DoingState(item));
        }
    }
}
