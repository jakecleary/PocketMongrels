using System;
using System.Collections.Generic;
using JakeCleary.PocketMongrels.Core;

namespace JakeCleary.PocketMongrels.Data
{
    public interface IUserRepository
    {
        List<User> All();

        User ByGuid(Guid id);

        bool Add(User user);

        bool Remove(User user);
    }
}