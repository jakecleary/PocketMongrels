using System;

namespace JakeCleary.PocketMongrels.Core.Entity
{
    public class Animal : IGloballyUniqueEntity
    {
        private const int ScorePrecision = 2;
        private const int MaxScore = 10;

        public Guid Guid { get; } = Guid.NewGuid();

        public string Name { get; set; }

        public Type Type { get; set; }

        public double Hunger
        {
            get
            {
                var daysSinceLastFeed = (DateTime.UtcNow - LastFeed).TotalHours;
                var hungerScore = daysSinceLastFeed * HungerModifier;
                var roundedHungerScore = Math.Round(hungerScore, ScorePrecision);

                return Math.Min(MaxScore, roundedHungerScore);
            }
        }

        public double Happiness
        {
            get
            {
                var daysSinceLastPet = (DateTime.UtcNow - LastPet).TotalHours;
                var happinessScore = daysSinceLastPet * HappinessModifier;
                var roundedHappinessScore = Math.Round(happinessScore, ScorePrecision);

                return Math.Min(MaxScore, roundedHappinessScore);
            }
        }

        public DateTime LastFeed { get; set; } = DateTime.UtcNow;

        public DateTime LastPet { get; set; } = DateTime.UtcNow;

        private double HungerModifier
        {
            get
            {
                switch (Type)
                {
                    case Type.Fast:
                        return 1.5;
                    case Type.Lazy:
                        return 0.5;
                    case Type.Smart:
                        return 1.5;
                    default:
                        return 1.0;
                }
            }   
        }

        private double HappinessModifier
        {
            get
            {
                switch (Type)
                {
                    case Type.Fast:
                        return 0.5;
                    case Type.Lazy:
                        return 1.5;
                    case Type.Smart:
                        return 1.5;
                    default:
                        return 1.0;
                }
            }
        }
    }

    public enum Type
    {
        Fast,
        Lazy,
        Smart
    };
}