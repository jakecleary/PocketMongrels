using System;
using System.Collections.Generic;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public class User
    {
        public Guid Uuid { get; } = Guid.NewGuid();

        public string Name { get; set; }

        public List<Animal> Animals { get; set; }
    }
}