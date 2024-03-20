using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backlog.States
{
    public class DoingState(Item item) : ItemState(item)
    {
        public override void SetState()
        {
            item.ChangeState(new DoingState(item));
        }

        public override void NextState()
        {
            item.ChangeState(new DoneState(item));
        }

        public override void PreviousState()
        {
            item.ChangeState(new ToDoState(item));
        }
    }
}
