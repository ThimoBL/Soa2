using Domain.Roles;
using Domain.Sprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Rapport.RapportStructure
{
    public class Body
    {
        public IList<User> Users = new List<User>();
        public IList<Sprint> Sprints = new List<Sprint>();

        public void AddUser(User user)
        {
            Users.Add(user);
        }

        public void AddSprint(Sprint sprint)
        {
            Sprints.Add(sprint);
        }
    }
}
