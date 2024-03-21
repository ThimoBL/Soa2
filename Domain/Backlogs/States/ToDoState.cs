using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backlogs.States
{
    public class ToDoState(Item item) : ItemState(item)
    {
        public override void SetState()
        {
            item.ChangeState(new ToDoState(item));
        }

        public override void NextState()
        {
            item.ChangeState(new DoingState(item));
        }

        public override void PreviousState()
        {
            throw new NotImplementedException();
        }
    }
}
