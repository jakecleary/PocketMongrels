using System.Collections.Generic;
using System.Net.Http;
using JakeCleary.PocketMongrels.Messages;
using JakeCleary.PocketMongrels.Server;
using Microsoft.Owin.Testing;
using NUnit.Framework;

namespace JakeCleary.PocketMongrels.ApiTests
{
    public class ApiTests
    {
        private TestServer _server;

        [TestFixtureSetUp]
        public void FixtureInit()
        {
            _server = TestServer.Create<Startup>();
        }

        [TestFixtureTearDown]
        public void FixtureDispose()
        {
            _server.Dispose();
        }

        [Test]
        public void TestGetAllValues()
        {
            var response = _server.HttpClient.GetAsync("/api/users").Result;

            Assert.That(response.IsSuccessStatusCode, Is.True);

            var result = response.Content.ReadAsAsync<IEnumerable<UserResponse>>().Result;
            
            // CollectionAssert.AreEqual(new [] {"value1", "value2"}, result);
        }
    }
}
