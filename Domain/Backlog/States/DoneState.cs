using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backlog.States
{
    public class DoneState(Item item) : ItemState(item)
    {
        public override void SetState()
        {
            item.ChangeState(new DoneState(item));
        }

        public override void NextState()
        {
            item.ChangeState(new ReadyForTestingState(item));
        }

        public override void PreviousState()
        {
            item.ChangeState(new DoingState(item));
        }
    }
}
