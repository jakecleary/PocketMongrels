using System;
using System.Collections.Generic;
using System.Linq;
using JakeCleary.PocketMongrels.Core;

namespace JakeCleary.PocketMongrels.Data.InMemory
{
    public class AnimalRepository : IAnimalRepository
    {
        private List<Animal> _animals;

        public List<Animal> All()
        {
            return _animals;
        }

        public Animal ByGuid(Guid id)
        {
            return _animals.FirstOrDefault(a => a.Id == id);
        }

        public bool Add(Animal animal)
        {
            _animals.Add(animal);

            return true;
        }

        public bool Remove(Animal animal)
        {
            _animals.Remove(animal);

            return true;
        }
    }
}