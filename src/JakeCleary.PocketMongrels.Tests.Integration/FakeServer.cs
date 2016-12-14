using System;
using System.Net.Http;
using System.Text;
using JakeCleary.PocketMongrels.Api;
using Microsoft.Owin.Testing;

namespace JakeCleary.PocketMongrels.Tests.Integration
{
    class FakeServer
    {
        public TestServer TestServer { get; }
        private HttpRequestMessage _request;

        public FakeServer()
        {
            TestServer = TestServer.Create<Startup>();
            TestServer.HttpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            _request = null;
        }

        public FakeServer NewRequestTo(string path)
        {
            _request = new HttpRequestMessage();
            _request.RequestUri = new Uri($"http://localhost{path}");

            return this;
        }

        public FakeServer Method(HttpMethod method)
        {
            _request.Method = method;

            return this;
        }

        public ApiResponse Send(string content = null)
        {
            var response = PrepareAndSendRequest(content);

            return ApiResponse.From(response);
        }

        public ApiResponse<TResource> Send<TResource>(string content = null)
        {
            var response = PrepareAndSendRequest(content);

            return ApiResponse<TResource>.From(response);
        }

        private HttpResponseMessage PrepareAndSendRequest(string content = null)
        {
            if (content != null)
            {
                _request.Content = new StringContent(content, Encoding.UTF8, "application/json");
            }

            var response = TestServer.HttpClient.SendAsync(_request).Result;

            _request = null;

            return response;
        }
    }
}
