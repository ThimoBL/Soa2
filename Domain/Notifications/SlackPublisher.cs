using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Notifications.Interfaces;

namespace Domain.Notifications
{
    public class SlackPublisher : IPublisher
    {
        public void Notify()
        {
            Console.WriteLine($"Slack message send");
        }
    }
}
