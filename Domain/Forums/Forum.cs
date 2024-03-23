using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Forums
{
    public class Forum(IList<Threads> threads)
    {
        public IList<Threads> Threads { get; set; } = threads;

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
