using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;

namespace Domain.Notifications.Interfaces
{
    public interface INotificationService
    {
        void Subscribe(User user);
        void Unsubscribe(User user);
        void SendNotification(string message);
    }
}
