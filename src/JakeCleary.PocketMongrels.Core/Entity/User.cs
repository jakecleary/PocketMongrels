using System;
using System.Collections.Generic;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public class User : IGloballyUniqueEntity
    {
        public Guid Id { get; } = Guid.NewGuid();

        public string Name { get; set; }

        public List<Animal> Animals { get; }

        public User()
        {
            Animals = new List<Animal>();
        }
    }
}