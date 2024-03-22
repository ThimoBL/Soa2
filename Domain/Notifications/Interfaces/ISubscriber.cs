using Domain.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Notifications.Interfaces
{
    public interface ISubscriber
    {
        void Notify();
        void AddPreferences(IPublisher preference);
    }
}
