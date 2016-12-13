using System;
using System.Net;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantANewPet
{
    class WhenISpecifyAnInvalidType
    {
        private FakeServer _server;
        private ApiResponse<Animal> _response;
        private Guid _ownerId;

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
                .Post<Animal>("{'Name': 'Snuffles the Rabbit', 'Type': 99}");
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
