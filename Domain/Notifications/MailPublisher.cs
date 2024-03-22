using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Notifications.Interfaces;

namespace Domain.Notifications
{
    public class MailPublisher : IPublisher
    {
        public void SendNotification(string message)
        {
            Console.WriteLine("Mail notification: " + message);
        }
    }
}
