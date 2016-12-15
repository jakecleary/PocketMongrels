using System;
using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantANewPet
{
    [TestFixture]
    class WhenISpecifyAnInvalidType
    {
        private FakeServer _server;
        private ApiResponse<Animal> _response;
        private Guid _ownerId;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            var userResponse = _server
                .NewRequestTo("/api/users/")
                .Method(HttpMethod.Post)
                .Send<User>("{'Name': 'Jake'}");

            _ownerId = userResponse.Resource.Id;

            _response = _server
                .NewRequestTo($"/api/users/{_ownerId}/animals")
                .Method(HttpMethod.Post)
                .Send<Animal>("{'Name': 'Snuffles the Rabbit', 'Type': 'Stupid'}");
        }

        [Test]
        public void ThenTheRequestIsUnsuccessful()
        {
            Assert.That(_response.Success, Is.False);
        }

        [Test]
        public void ThenTheResponseCodeIsAppropriate()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }
    }
}
