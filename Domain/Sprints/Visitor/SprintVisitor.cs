using Domain.Sprints.States;

namespace Domain.Sprints.Visitor
{
    internal class SprintVisitor(Sprint sprint) : ISprintVisitor
    {
        public void VisitReview(ReviewSprint review)
        {
            Console.WriteLine("Ending sprint with review");
            //ToDo: Do some finished review logic
        }

        public void VisitRelease(ReleaseSprint release)
        {
            sprint.ChangeState(new ClosedState(sprint));
            Console.WriteLine("Ending sprint with release");
            //ToDo: Do some finished release logic
        }
    }
}
