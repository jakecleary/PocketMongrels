using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;

namespace JakeCleary.PocketMongrels.Tests.Integration
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public HttpResponseMessage RawResponse { get; set; }

        public static ApiResponse From(HttpResponseMessage response)
        {
            return new ApiResponse
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                RawResponse = response,
            };
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Resource { get; set; }
        public Error Errors { get; set; }
        public string Location => RawResponse.Headers.Location.ToString();

        public new static ApiResponse<T> From(HttpResponseMessage response)
        {
            var apiResponse = new ApiResponse<T>
            {
                Success = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode,
                RawResponse = response,
            };

            if (!apiResponse.Success)
            {
                apiResponse.Errors = response.Content.ReadAsAsync<Error>().Result;
            }

            else
            {
                apiResponse.Resource = response.Content.ReadAsAsync<T>().Result;
            }

            return apiResponse;
        }
    }
}