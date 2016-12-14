using System;
using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenAPet
{
    [TestFixture]
    class WhenIPetIt
    {
        private FakeServer _server;
        private ApiResponse _response;
        private Animal _animal;
        private DateTime _animalPetTime;

        [TestFixtureSetUp]
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
                .Send<Animal>("{'Name': 'Sid the Sloth', 'Type': 'Lazy'}");

            var animalId = animalResponse.Resource.Id;
            _animalPetTime = animalResponse.Resource.LastPet;

            _response = _server
                .NewRequestTo($"/api/users/{userId}/animals/{animalId}/pet")
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
        public void ThenTheAnimalIsHappy()
        {
            Assert.That(_animal.Happiness, Is.EqualTo(10));
        }

        [Test]
        public void ThenTheAnimalsLastPetTimeIsUpdated()
        {
            Assert.That(_animal.LastPet, Is.GreaterThan(_animalPetTime));
        }
    }
}
