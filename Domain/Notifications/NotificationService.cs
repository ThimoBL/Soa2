using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Notifications.Interfaces;
using Domain.Roles;

namespace Domain.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly IList<ISubscriber> _listeners = new List<ISubscriber>();

        public void Subscribe(User user)
        {
            _listeners.Add(user);
        }

        public void Unsubscribe(User user)
        {
            _listeners.Remove(user);
        }

        public void SendNotification(string message)
        {
            foreach (var listener in _listeners)
            {
                listener.Notify();
            }
        }
    }
}
