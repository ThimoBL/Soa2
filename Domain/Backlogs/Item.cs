using Domain.Backlogs.States;
using Domain.Forums;
using Domain.Roles;

namespace Domain.Backlogs
{
    public abstract class Item
    {
        protected Item(string title, string description, Developer developer)
        {
            Title = title;
            Description = description;
            Developer = developer;
            Status = new ToDoState(this);
        }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; }
        public string Description { get; set; }
        public Developer Developer { get; set; }
        public ItemState Status { get; set; }
        public Threads Thread { get; set; }
        public void ChangeState(ItemState itemState)
        {
            Status = itemState;
        }

        public void AddThread(Threads threads)
        {
            Thread = threads;
        }
    }
}
