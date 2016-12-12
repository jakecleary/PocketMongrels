using System;
using System.Net;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.GivenIWantANewPet
{
    class WhenCreatingThePet
    {
        private FakeServer _server;
        private Guid _ownerId;
        private ApiResponse<Animal> _response;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            var userResponse = _server
                .NewRequestTo("/api/users/")
                .Post<User>("{'Name': 'Jake'}");

            _ownerId = userResponse.Resource.Id;

            _response = _server
                .NewRequestTo($"/api/users/{_ownerId}/animals")
                .Post<Animal>("{'Name': 'Snuffles the Rabbit', 'Type': 0}");
        }

        [Test]
        public void ThenTheResponseIsSuccessful()
        {
            Assert.That(_response.Success, Is.True);
        }

        [Test]
        public void ThenTheStatusCodeIsAppropriate()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void ThenTheAnimalIsCreatedExactly()
        {
            Assert.That(_response.Resource.Id, Is.TypeOf<Guid>());
            Assert.That(_response.Resource.Name, Is.EqualTo("Snuffles the Rabbit"));
            Assert.That(_response.Resource.Type, Is.EqualTo(0));
        }

        [Test]
        public void ThenTheAnimalIsInitializedCorrectly()
        {
            // Make sure the dates are recent.
            var now = DateTime.UtcNow;
            var limit = DateTime.UtcNow.AddSeconds(-2);

            Assert.That(_response.Resource.Hunger, Is.EqualTo(0.5));
            Assert.That(_response.Resource.Happiness, Is.EqualTo(0.5));
            Assert.That(_response.Resource.LastFeed, Is.GreaterThan(limit).And.LessThan(now));
            Assert.That(_response.Resource.LastPet, Is.GreaterThan(limit).And.LessThan(now));
            Assert.That(_response.Resource.Born, Is.GreaterThan(limit).And.LessThan(now));
        }

        [Test]
        public void ThenTheResourceHasAUri()
        {
            var id = _response.Resource.Id;

            Assert.That(_response.Location, Is.StringEnding($"/api/users/{_ownerId}/animals/{id}"));
        }
    }
}
