using System;
using System.Collections.Generic;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public class User : IGloballyUniqueEntity
    {
        public Guid Guid { get; } = Guid.NewGuid();

        public string Name { get; set; }

        public List<Animal> Animals { get; set; }
    }
}