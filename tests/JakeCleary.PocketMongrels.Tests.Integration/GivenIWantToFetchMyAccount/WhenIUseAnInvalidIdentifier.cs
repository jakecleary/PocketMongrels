﻿using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantToFetchMyAccount
{
    [TestFixture]
    class WhenIUseAnInvalidIdentifier
    {
        private FakeServer _server;
        private ApiResponse<User> _response;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            _response = _server
                .NewRequestTo("/api/users/this-is-not-a-valid-id")
                .Method(HttpMethod.Get)
                .Send<User>();
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
