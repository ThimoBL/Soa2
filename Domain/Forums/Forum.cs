using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forums
{
    public class Forum()
    {
        public IList<Threads> Threads { get; set; } = new List<Threads>();

        public List<Threads> GetAllThreads() => Threads.ToList();

        public void AddThread(Threads thread)
        {
            Threads.Add(thread);
        }

        public void RemoveThread(Threads thread)
        {
            Threads.Remove(thread);
        }
    }
}