using System;

namespace Domain.Sprints.Visitor
{
    internal interface ISprintVisitor
    {
        void VisitReview(ReviewSprint review);
        void VisitRelease(ReleaseSprint release);
    }
}
