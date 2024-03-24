using System;

namespace Domain.Sprints.Visitor
{
    public interface ISprintVisitor
    {
        void VisitReview(ReviewSprint review);
        void VisitRelease(ReleaseSprint release);
    }
}
