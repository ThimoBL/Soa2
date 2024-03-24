using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.GeneralModels;
using Domain.Notifications.Interfaces;
using Domain.Pipelines;
using Domain.Pipelines.Visitor;
using Domain.Roles;
using Domain.VersionControl.Interfaces;

namespace Domain.Sprints.Factory
{
    public class SprintFactory() : ISprintFactory
    {
        public Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster,
            Tester tester, Pipeline pipeline, IGitStrategy gitStrategy, Project project, SprintType sprintType,
            INotificationService notificationService) =>
            sprintType switch
            {
                SprintType.Release => new ReleaseSprint(title, startDate, endDate, scrumMaster,
                    tester, pipeline, gitStrategy, project, notificationService),
                SprintType.Review => new ReviewSprint(title, startDate, endDate, scrumMaster,
                    tester, pipeline, gitStrategy, project, notificationService),
                _ => throw new ArgumentException("Invalid sprint type")
            };
    }
}