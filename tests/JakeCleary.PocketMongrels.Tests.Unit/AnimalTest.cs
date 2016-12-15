using System;
using JakeCleary.PocketMongrels.Core;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Unit
{
    [TestFixture]
    public class AnimalTest
    {
        [Test]
        public void TestAnimalsStartNeutral()
        {
            var animal = new Animal();

            // Animals should start neutral on both metrics.
            Assert.That(animal.Hunger, Is.EqualTo(0.5));
            Assert.That(animal.Happiness, Is.EqualTo(0.5));
        }

        [Test]
        public void TestAnimalsGetHungry()
        {
            var animal = new Animal { LastFeed = DateTime.UtcNow.AddHours(-1) };

            // Animal hunger should increase over time.
            Assert.That(animal.Hunger, Is.GreaterThan(0.5));
        }

        [Test]
        public void TestAnimalsGetSad()
        {
            var animal = new Animal { LastPet = DateTime.UtcNow.AddHours(-1) };

            // Animal hunger should increase over time.
            Assert.That(animal.Happiness, Is.LessThan(0.5));
        }

        [Test]
        public void TestMetricsBottomOut()
        {
            var animal = new Animal
            {
                LastFeed = DateTime.UtcNow.AddYears(-1),
                LastPet = DateTime.UtcNow.AddYears(-1)
            };

            Assert.That(animal.Hunger, Is.EqualTo(10));
            Assert.That(animal.Happiness, Is.EqualTo(0));
        }

        [Test]
        public void TestMetricsTopOut()
        {
            var animal = new Animal
            {
                Hunger = Animal.MinScore,
                Happiness = Animal.MaxScore
            };

            Assert.That(animal.Hunger, Is.EqualTo(0));
            Assert.That(animal.Happiness, Is.EqualTo(10));
        }
    }
}
