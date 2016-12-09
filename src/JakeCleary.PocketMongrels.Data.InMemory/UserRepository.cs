using System;
using System.Collections.Generic;
using System.Linq;
using JakeCleary.PocketMongrels.Core;

namespace JakeCleary.PocketMongrels.Data.InMemory
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>();

        public List<User> All()
        {
            return _users;
        }

        public User ByGuid(Guid id)
        {
            return _users.FirstOrDefault(e => e.Id == id);
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