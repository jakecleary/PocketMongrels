using System;
using System.Collections.Generic;
using System.Linq;
using JakeCleary.PocketMongrels.Core.Entity;

namespace JakeCleary.PocketMongrels.Data.InMemory
{
    public class Repository<T> : IRepository<T> where T : IGloballyUniqueEntity
    {
        private readonly List<T> _entities = new List<T>();
         
        public List<T> All()
        {
            return _entities;
        }

        public T ByGuid(Guid guid)
        {
            return _entities.FirstOrDefault(e => e.Id == guid);
        }

        public bool Add(T entity)
        {
            _entities.Add(entity);

            return true;
        }

        public bool Remove(T entity)
        {
            _entities.Remove(entity);

            return true;
        }
    }
}
