﻿using System;

namespace JakeCleary.PocketMongrels.Core
{
    public class Animal : IGloballyUniqueEntity
    {
        private const int ScorePrecision = 2;
        public const int MinScore = 0;
        public const int MaxScore = 10;

        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public AnimalType Type { get; set; }

        public Animal()
        {
            // Start off with neutral hunger and happiness.
            Hunger = 0.5;
            Happiness = 0.5;
            LastFeed = DateTime.UtcNow;
            LastPet = DateTime.UtcNow;
            Born = DateTime.UtcNow;
        }

        private double _hunger;
        public double Hunger
        {
            get { return CalculateMetricScore(Action.Feed); }
            set { _hunger = value; }
        }

        private double _happiness;
        public double Happiness
        {
            get { return CalculateMetricScore(Action.Pet); }
            set { _happiness = value; }
        }

        public DateTime LastFeed { get; set; }

        public DateTime LastPet { get; set; }

        public DateTime Born { get; }

        private double HungerModifier
        {
            get
            {
                switch (Type)
                {
                    case AnimalType.Fast:
                        return 1.5;
                    case AnimalType.Lazy:
                        return 0.5;
                    case AnimalType.Smart:
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
                    case AnimalType.Fast:
                        return 0.5;
                    case AnimalType.Lazy:
                        return 1.5;
                    case AnimalType.Smart:
                        return 1.5;
                    default:
                        return 1.0;
                }
            }
        }

        private double CalculateMetricScore(Action action)
        {
            double currentScore;
            double modifier;
            DateTime lastInteraction;

            switch (action)
            {
                case Action.Feed:
                    currentScore = _hunger;
                    lastInteraction = LastFeed;
                    modifier = HungerModifier;
                    break;
                case Action.Pet:
                    currentScore = _happiness;
                    lastInteraction = LastPet;
                    modifier = HappinessModifier;
                    break;
                default:
                    throw new Exception($"Illigal enum value {action}");
            }

            // Calculate the animal's metric score.
            var score = (DateTime.UtcNow - lastInteraction).TotalHours;
            score = score * modifier;
            score = Math.Round(score, ScorePrecision);

            // Hunger scores go up, happiness scores go down.
            score = action == Action.Feed ? (currentScore + score) : (currentScore - score);

            // Return the score clamped between 0 & 10.
            return (Math.Min(Math.Max(score, MinScore), MaxScore));
        }
    }

    public enum AnimalType
    {
        Fast,
        Lazy,
        Smart
    }

    public enum Action
    {
        Feed,
        Pet
    }
}