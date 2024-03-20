using Domain.Sprints.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Sprints
{
    public class SprintVisitor(Sprint sprint) : ISprintVisitor
    {
        public void Visit(ReviewSprint review)
        {
            Console.WriteLine("Ending sprint with review");
            //ToDo: Do some finished review logic
        }

        public void Visit(ReleaseSprint release)
        {
            sprint.ChangeState(new ClosedState(sprint));
            Console.WriteLine("Ending sprint with release");
            //ToDo: Do some finished release logic
        }
    }
}
