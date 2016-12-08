using System;
using System.Net;
using System.Net.Http;
using System.Text;
using JakeCleary.PocketMongrels.Api;
using JakeCleary.PocketMongrels.Api.Resourses;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.ApiTests
{
    [TestFixture]
    public class ApiTests
    {
        private TestServer _server;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = TestServer.Create<Startup>();
            _server.HttpClient.BaseAddress = new Uri("http://localhost");
            _server.HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [Test]
        public void TestCreateNewUser()
        {
            // We're going to create a new user...
            HttpRequestMessage newUserRequest;
            HttpResponseMessage newUserResponse;

            // And check that they're created properly.
            HttpRequestMessage getUserRequest;
            HttpResponseMessage getUserResponse;

            // Build the request.
            newUserRequest = new HttpRequestMessage(HttpMethod.Post, "/api/users");
            newUserRequest.Content = new StringContent("{'Name': 'Jake'}", Encoding.UTF8, "application/json");

            // Send the request and store it's response.
            newUserResponse = _server.HttpClient.SendAsync(newUserRequest).Result;

            // Get the location header and body.
            var location = newUserResponse.Headers.Location.ToString();
            var newUser = newUserResponse.Content.ReadAsAsync<User>().Result;

            // Check it worked.
            Assert.That(newUserResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(location, Is.StringMatching($"/api/users/{newUser.Id}"));
            Assert.That(newUser.Name, Is.EqualTo("Jake"));

            // Fetch the newly created User resourse.
            getUserRequest = new HttpRequestMessage(HttpMethod.Get, location);
            getUserResponse = _server.HttpClient.SendAsync(getUserRequest).Result;
            var user = getUserResponse.Content.ReadAsAsync<User>().Result;

            // Make sure the user's details are correct, including having no animals.
            Assert.That(getUserResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(user.Id, Is.EqualTo(newUser.Id));
            Assert.That(user.Name, Is.EqualTo("Jake"));
            Assert.That(user.Animals, Is.Empty);
        }
    }
}
