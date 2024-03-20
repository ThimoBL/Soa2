using Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlog.States;

namespace Domain.Backlog
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

        public void ChangeState(ItemState itemState)
        {
            Status = itemState;
        }
    }
}
