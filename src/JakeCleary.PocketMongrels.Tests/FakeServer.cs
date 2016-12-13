using System;
using System.Net.Http;
using System.Text;
using JakeCleary.PocketMongrels.Api;
using Microsoft.Owin.Testing;

namespace JakeCleary.PocketMongrels.Tests
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

        public ApiResponse<TResourse> Get<TResourse>()
        {
            _request.Method = HttpMethod.Get;

            return Send<TResourse>();
        }

        public ApiResponse<TResourse> Post<TResourse>(string content)
        {
            _request.Method = HttpMethod.Post;
            _request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            return Send<TResourse>();
        }

        public ApiResponse<TResourse> Put<TResourse>(string content)
        {
            _request.Method = HttpMethod.Put;
            _request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            return Send<TResourse>();
        }

        public ApiResponse<TResourse> Delete<TResourse>()
        {
            _request.Method = HttpMethod.Delete;

            return Send<TResourse>();
        }

        public ApiResponse<TResource> Send<TResource>()
        {
            var response = TestServer.HttpClient.SendAsync(_request).Result;
            _request = null;

            return ApiResponse<TResource>.From(response);
        }
    }
}
