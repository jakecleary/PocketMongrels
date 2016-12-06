using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JakeCleary.PocketMongrels.Core.Entity;

namespace JakeCleary.PocketMongrels.Data.Repository
{
    public class UserRepository : IRepository<User>
    {
        private readonly List<User> _users = new List<User>();
         
        public List<User> All()
        {
            return _users;
        }

        public User ByGuid(Guid guid)
        {
            return _users.FirstOrDefault(u => u.Uuid == guid);
        }

        public bool Add(User user)
        {
            _users.Add(user);

            return true;
        }

        public bool Remove(User user)
        {
            _users.Remove(user);

            return true;
        }
    }
}
