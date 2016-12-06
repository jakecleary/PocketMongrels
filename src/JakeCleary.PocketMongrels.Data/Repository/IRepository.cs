using System;
using System.Collections.Generic;
using JakeCleary.PocketMongrels.Core.Entity;

namespace JakeCleary.PocketMongrels.Data.Repository
{
    public interface IRepository<T>
    {
        List<T> All();

        T ByGuid(Guid guid);

        bool Add(T entity);

        bool Remove(T entity);
    }
}
