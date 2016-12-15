using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantToCheckTheSystem
{
    [TestFixture]
    class WhenIAskForTheStatus
    {
        private FakeServer _server;
        private ApiResponse _response;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            _response = _server
                .NewRequestTo("/system/status")
                .Method(HttpMethod.Get)
                .Send();
        }

        [Test]
        public void ThenTheRequestIsSuccessful()
        {
            Assert.That(_response.Success, Is.True);
        }

        [Test]
        public void ThenTheStatusCodeIsAppropriate()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void TheTheResponseBodyIsEmpty()
        {
            Assert.That(_response.RawResponse.Content.ReadAsStringAsync().Result, Does.Match(""));
        }
    }
}
