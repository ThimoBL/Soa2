using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Notifications.Interfaces;

namespace Domain.Roles
{
    public abstract class User(string name, string email, string password) : ISubscriber
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { get; set; } = password;
        public List<INotificationService> Preferences { get; set; } = new();
        public void Notify()
        {
            foreach (var preference in Preferences)
            {
                preference.SendNotification(Email);
            }
        }

        public void AddPreferences(INotificationService preference)
        {
            Preferences.Add(preference);
        }
    }
}
