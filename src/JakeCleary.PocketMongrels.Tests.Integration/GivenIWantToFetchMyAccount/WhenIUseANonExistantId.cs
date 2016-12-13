using System;
using System.Net;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantToFetchMyAccount
{
    [TestFixture]
    class WhenIUseANonExistantId
    {
        private FakeServer _server;
        private ApiResponse<User> _response;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            var id = Guid.NewGuid();
            var uri = $"/api/users/{id}";

            _response = _server
                .NewRequestTo(uri)
                .Get<User>();
        }

        [Test]
        public void ThenTheRequestFails()
        {
            Assert.That(_response.Success, Is.False);
        }

        [Test]
        public void ThenTheStatusCodeIsAppropriate()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
