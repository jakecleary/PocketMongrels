using System;
using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenAPet
{
    [TestFixture]
    class WhenIFeedIt
    {
        private FakeServer _server;
        private ApiResponse _response;
        private Animal _animal;
        private DateTime _animalFeedTime;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            var userResponse = _server
                .NewRequestTo("/api/users")
                .Method(HttpMethod.Post)
                .Send<User>("{'Name': 'Jake'}");

            var userId = userResponse.Resource.Id;

            var animalResponse = _server
                .NewRequestTo($"/api/users/{userId}/animals")
                .Method(HttpMethod.Post)
                .Send<Animal>("{'Name': 'Aurthur the Aardvark', 'Type': 'Smart'}");

            var animalId = animalResponse.Resource.Id;
            _animalFeedTime = animalResponse.Resource.LastFeed;

            _response = _server
                .NewRequestTo($"/api/users/{userId}/animals/{animalId}/feed")
                .Method(HttpMethod.Post)
                .Send();

            _animal = _server
                .NewRequestTo($"/api/users/{userId}/animals/{animalId}")
                .Method(HttpMethod.Get)
                .Send<Animal>()
                .Resource;
        }

        [Test]
        public void ThenTheRequestIsSuccessful()
        {
            Assert.That(_response.Success, Is.True);
        }

        [Test]
        public void ThenTheStatusCodeIsAppropriate()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NoContent));
        }

        [Test]
        public void ThenTheAnimalIsSatiated()
        {
            Assert.That(_animal.Hunger, Is.EqualTo(0));
        }

        [Test]
        public void ThenTheAnimalsLastFeedTimeIsUpdated()
        {
            Assert.That(_animal.LastFeed, Is.GreaterThan(_animalFeedTime));
        }
    }
}
