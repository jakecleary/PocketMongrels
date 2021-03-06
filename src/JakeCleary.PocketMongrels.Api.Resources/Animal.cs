﻿using System;

namespace JakeCleary.PocketMongrels.Api.Resourses
{
    public class Animal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public double Hunger { get; set; }
        public double Happiness { get; set; }
        public DateTime LastFeed { get; set; }
        public DateTime LastPet { get; set; }
        public DateTime Born { get; set; }

        public static Animal From(Core.Animal animal)
        {
            return new Animal
            {
                Id = animal.Id,
                Name = animal.Name,
                Type = animal.Type.ToString(),
                Hunger = animal.Hunger,
                Happiness = animal.Happiness,
                LastFeed = animal.LastFeed,
                LastPet = animal.LastPet,
                Born = animal.Born,
            };
        }
    }
}
