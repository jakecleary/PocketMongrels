using System;
using System.Collections.Generic;

namespace JakeCleary.PocketMongrels.Data
{
    public interface IRepository<T>
    {
        List<T> All();

        T ByGuid(Guid guid);

        bool Add(T entity);

        bool Remove(T entity);
    }
}
