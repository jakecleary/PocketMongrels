using System;
using System.Net;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantToCreateAnAccount
{
    [TestFixture]
    public class WhenIUseValidUserData
    {
        private FakeServer _server;
        private ApiResponse<User> _response;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            _response = _server
                .NewRequestTo("/api/users/")
                .Post<User>("{'Name': 'Jake'}");
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
        public void ThenTheUserIsCreatedExactly()
        {
            Assert.That(_response.Resource.Id, Is.TypeOf<Guid>());
            Assert.That(_response.Resource.Name, Is.EqualTo("Jake"));
            Assert.That(_response.Resource.Animals, Is.Empty);
        }

        [Test]
        public void ThenTheResourceHasAUri()
        {
            var id = _response.Resource.Id;

            Assert.That(_response.Location, Is.StringEnding($"/api/users/{id}"));
        }
    }
}
