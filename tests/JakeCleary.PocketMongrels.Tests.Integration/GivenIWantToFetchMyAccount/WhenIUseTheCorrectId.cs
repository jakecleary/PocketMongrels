﻿using System;
using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.Tests.Integration.GivenIWantToFetchMyAccount
{
    [TestFixture]
    class WhenIUseTheCorrectId
    {
        private FakeServer _server;
        private ApiResponse<User> _response;
        private Guid _userId;

        [OneTimeSetUp]
        public void FixtureInit()
        {
            _server = new FakeServer();

            _userId = _server
                .NewRequestTo("/api/users/")
                .Method(HttpMethod.Post)
                .Send<User>("{'Name': 'Jake'}")
                .Resource.Id;

            var uri = $"/api/users/{_userId}";

            _response = _server
                .NewRequestTo(uri)
                .Method(HttpMethod.Get)
                .Send<User>();
        }

        [Test]
        public void ThenTheResponseIsSuccessful()
        {
            Assert.That(_response.Success, Is.True);
        }

        [Test]
        public void ThenTheStatusCodeIsAppropriate()
        {
            Assert.That(_response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void ThenTheUserIdMatchesTheUri()
        {
            Assert.That(_response.Resource.Id, Is.EqualTo(_userId));
        }
    }
}
