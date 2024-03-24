using Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Pipelines;
using Domain.VersionControl.Interfaces;
using Domain.GeneralModels;
using Domain.Notifications.Interfaces;
using Domain.Pipelines.Visitor;

namespace Domain.Sprints.Factory
{
    public interface ISprintFactory
    {
        Sprint CreateSprint(string title, DateTime startDate, DateTime endDate, ScrumMaster scrumMaster, Tester tester,
            Pipeline pipeline, IGitStrategy gitStrategy, Project project, SprintType sprintType,
            INotificationService notificationService);
    }
}