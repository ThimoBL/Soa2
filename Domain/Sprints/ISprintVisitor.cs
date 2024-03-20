using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints
{
    public interface ISprintVisitor
    {
        void Visit(ReviewSprint review);
        void Visit(ReleaseSprint release);
    }
}
