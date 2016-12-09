using System;
using System.Collections.Generic;
using JakeCleary.PocketMongrels.Core;

namespace JakeCleary.PocketMongrels.Data
{
    public interface IAnimalRepository
    {
        List<Animal> All();

        Animal ByGuid(Guid id);

        bool Add(Animal animal);

        bool Remove(Animal animal);
    }
}