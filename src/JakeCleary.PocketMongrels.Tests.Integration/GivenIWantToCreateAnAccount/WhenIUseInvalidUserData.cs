using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantToCreateAnAccount
{
    [TestFixture]
    public class WhenIUseInvalidUserData
    {
        private FakeServer _server;
        private ApiResponse<User> _response;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            _response = _server
                .NewRequestTo("/api/users")
                .Method(HttpMethod.Post)
                .Send<User>("{'Name': 'UsernameWithTooManyCharacters'}");
        }

        [Test]
        public void ThenTheRequestFails()
        {
            Assert.That(_response.Success, Is.False);
        }

        [Test]
        public void ThenTheStatusCodeIsAppropriate()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void ThenAHelpfulErrorMessageIsReturned()
        {
            Assert.That(_response.Errors.Message, Is.EqualTo("The request is invalid."));
        }
    }
}