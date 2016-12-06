using System;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public class Type
    {
        public Guid Uuid { get; } = Guid.NewGuid();

        public string Name { get; set; }

        public int HappinessModifier { get; }

        public int HungerModifier { get; }
    }
}