using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints.States
{
    public abstract class SprintState(Sprint sprint)
    {
        public Sprint Sprint { get; set; } = sprint;

        public abstract void SetState();
        public abstract void NextState();
    }
}
