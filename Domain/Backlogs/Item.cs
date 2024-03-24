using Domain.Backlogs.States;
using Domain.Forums;
using Domain.Notifications;
using Domain.Notifications.Interfaces;
using Domain.Roles;
using Domain.Sprints;

namespace Domain.Backlogs
{
    public abstract class Item
    {
        private readonly Sprint _sprint;

        protected Item(string title, string description, Developer developer, Sprint sprint,
            INotificationService notificationService)
        {
            Title = title;
            Description = description;
            Developer = developer;
            Status = new ToDoState(this);

            //Navigation property
            _sprint = sprint;
            NotificationService = notificationService;
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public Developer? Developer { get; set; }
        public ItemState Status { get; set; }
        public Threads? Thread { get; set; }
        public INotificationService NotificationService { get; set; }

        public void ChangeState(ItemState itemState)
        {
            if (Status.GetType() == typeof(TestingState) && itemState.GetType() == typeof(ToDoState))
            {
                GetSprint().NotifyScrumMaster("Item is back to ToDo state");
            }
            Status = itemState;
        }

        public void NextState() => Status.NextState();

        public void AddThread(Threads threads)
        {
            Thread = threads;
        }

        public Sprint GetSprint() => _sprint;
    }
}