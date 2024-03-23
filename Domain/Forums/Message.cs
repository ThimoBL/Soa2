using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Roles;

namespace Domain.Forums
{
    public class Message(string content, User author)
    {
        public string Content { get; set; } = content;
        public User Author { get; set; } = author;
    }
}
