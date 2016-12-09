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
            ///////
            // User
            ///////

            // Create a new user...
            var newUserRequest = new HttpRequestMessage(HttpMethod.Post, "/api/users");
            newUserRequest.Content = new StringContent("{'Name': 'Jake'}", Encoding.UTF8, "application/json");
            var newUserResponse = _server.HttpClient.SendAsync(newUserRequest).Result;

            // Get the location header and body.
            var location = newUserResponse.Headers.Location.ToString();
            var newUser = newUserResponse.Content.ReadAsAsync<User>().Result;

            // ...check that they're created properly.
            Assert.That(newUserResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(location, Is.StringMatching($"/api/users/{newUser.Id}"));
            Assert.That(newUser.Name, Is.EqualTo("Jake"));

            // Fetch the newly created User resource...
            var getUserRequest = new HttpRequestMessage(HttpMethod.Get, location);
            var getUserResponse = _server.HttpClient.SendAsync(getUserRequest).Result;
            var user = getUserResponse.Content.ReadAsAsync<User>().Result;

            // ... and make sure the user's details are correct.
            Assert.That(getUserResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(user.Id, Is.EqualTo(newUser.Id));
            Assert.That(user.Name, Is.EqualTo("Jake"));
            Assert.That(user.Animals, Is.Empty);

            /////////
            // Animal
            /////////

            // Create a 'Fast' rabbit called Snuffles.
            var jsonPayload = "{'Name': 'Snuffles the Rabbit', 'Type': 0}";
            var createAnimalRequest = new HttpRequestMessage(HttpMethod.Post, $"/api/users/{user.Id}/animals");
            createAnimalRequest.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            var createAnimalResponse = _server.HttpClient.SendAsync(createAnimalRequest).Result;

            // Get the location header and body.
            var newAnimal = createAnimalResponse.Content.ReadAsAsync<Animal>().Result;
            var newAnimalLocation = createAnimalResponse.Headers.Location.ToString();

            Assert.That(createAnimalResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));
            Assert.That(newAnimalLocation, Is.StringMatching($"/api/users/{user.Id}/animals/{newAnimal.Id}"));
            Assert.That(newAnimal.Name, Is.EqualTo("Snuffles the Rabbit"));
            Assert.That(newAnimal.Type, Is.EqualTo(0));

            // Fetch the newly created Animal resource...
            var getAnimalRequest = new HttpRequestMessage(HttpMethod.Get, newAnimalLocation);
            var getAnimalResponse = _server.HttpClient.SendAsync(getAnimalRequest).Result;
            var animal = getAnimalResponse.Content.ReadAsAsync<Animal>().Result;

            // ...and make sure the details are correct.
            Assert.That(getAnimalResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(animal.Id, Is.EqualTo(newAnimal.Id));
            Assert.That(animal.Name, Is.EqualTo("Snuffles the Rabbit"));
            Assert.That(animal.Type, Is.EqualTo(0));
        }
    }
}
