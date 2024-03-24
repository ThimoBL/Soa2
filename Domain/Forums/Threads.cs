using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Backlogs;

namespace Domain.Forums
{
    public class Threads(string title, string description, Item backlogItem)
    {
        public string Title { get; set; } = title;
        public string Description { get; set; } = description;
        public Item BacklogItem { get; set; } = backlogItem;
        public IList<Message> Messages { get; set; } = new List<Message>();

        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }

        public void ReadAllMessages()
        {
            foreach (var message in Messages)
            {
                Console.WriteLine($"Thread - Message: {message.Content} - Written By: {message.Author.Name}");
            }
        }
    }
}