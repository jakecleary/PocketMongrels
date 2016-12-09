using System;
using System.Collections.Generic;
using JakeCleary.PocketMongrels.Core;
using JakeCleary.PocketMongrels.Data;

namespace JakeCleary.PocketMongrels.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> All()
        {
            return _userRepository.All();
        }

        public User FindById(Guid id)
        {
            return _userRepository.ByGuid(id);
        }

        public bool Add(User user)
        {
            _userRepository.Add(user);

            return true;
        }

        public bool Remove(User user)
        {
            _userRepository.Remove(user);

            return true;
        }
    }
}
