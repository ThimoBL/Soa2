using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Backlog
{
    public abstract class Backlog
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public IList<BacklogItem> Items { get; set; } = new List<BacklogItem>();

        public virtual void AddBacklogItem(BacklogItem item)
        {
            Items.Add(item);
        }

        public virtual void RemoveBacklogItem(BacklogItem item)
        {
            Items.Remove(item);
        }

        public virtual void ClearBacklog()
        {
            Items.Clear();
        }
    }
}
