using System.Net;
using System.Net.Http;
using JakeCleary.PocketMongrels.Api.Resourses;

namespace JakeCleary.PocketMongrels.Tests
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T Resource { get; set; }
        public Error Errors { get; set; }
        public HttpResponseMessage RawResponse { get; set; }
        public string Location => RawResponse.Headers.Location.ToString();

        public static ApiResponse<T> From(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                return new ApiResponse<T>
                {
                    Success = false,
                    StatusCode = response.StatusCode,
                    Errors = response.Content.ReadAsAsync<Error>().Result,
                    RawResponse = response
                };
            }

            return new ApiResponse<T>
            {
                Success = true,
                StatusCode = response.StatusCode,
                Resource = response.Content.ReadAsAsync<T>().Result,
                RawResponse = response
            };
        }
    }
}