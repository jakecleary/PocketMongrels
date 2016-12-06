using System;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public class Animal : IGloballyUniqueEntity
    {
        public Guid Guid { get; } = Guid.NewGuid();

        public string Name { get; set; }

        public Type Type { get; set; }

        public double Hunger
        {
            get { return 0.1; }
        }

        public DateTime LastFeed { get; set; }

        public DateTime LastPet { get; set; }
    }
}