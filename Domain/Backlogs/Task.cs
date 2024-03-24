using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Notifications.Interfaces;
using Domain.Roles;
using Domain.Sprints;

namespace Domain.Backlogs
{
    public class Task : Item
    {
        public Task(string title, string description, Developer developer, Sprint sprint,
            INotificationService notificationService) : base(title, description, developer, sprint, notificationService)
        {
        }

        //Navigation property
        private readonly BacklogItem _backlogItem;
    }
}