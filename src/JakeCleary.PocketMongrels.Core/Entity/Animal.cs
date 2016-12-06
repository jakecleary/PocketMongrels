using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public class Animal
    {
        public Guid Uuid { get; } = Guid.NewGuid();

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