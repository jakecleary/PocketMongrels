using System;
using JakeCleary.PocketMongrels.Core;

namespace JakeCleary.PocketMongrels.Services
{
    public class AnimalService
    {
        public void Feed(Animal animal)
        {
            animal.Hunger = Animal.MinScore;
            animal.LastFeed = DateTime.UtcNow;
        }

        public void Pet(Animal animal)
        {
            animal.Happiness = Animal.MaxScore;
            animal.LastPet = DateTime.UtcNow;
        }
    }
}
