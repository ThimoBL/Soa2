using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backlogs.States
{
    public abstract class ItemState(Item item)
    {
        public Item item { get; set; } = item;

        public abstract void SetState();
        public abstract void NextState();
        public abstract void PreviousState();
    }
}
