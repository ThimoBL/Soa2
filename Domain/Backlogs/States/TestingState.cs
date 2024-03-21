using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backlogs.States
{
    public class TestingState(Item item) : ItemState(item)
    {
        public override void SetState()
        {
            throw new NotImplementedException();
        }

        public override void NextState()
        {
            throw new NotImplementedException();
        }

        public override void PreviousState()
        {
            throw new NotImplementedException();
        }
    }
}
