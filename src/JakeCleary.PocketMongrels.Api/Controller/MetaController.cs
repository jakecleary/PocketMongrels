using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JakeCleary.PocketMongrels.Api.Controller
{
    public class MetaController : ApiController
    {
        [Route("*")]
        [HttpOptions]
        public IHttpActionResult Options()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new StringContent(string.Empty);
            response.Content.Headers.Add("Allow", new[] {"GET", "POST", "DELETE", "OPTIONS"});
            response.Content.Headers.ContentType = null;

            return ResponseMessage(response);
        }

        [HttpGet]
        [Route("system/status")]
        public IHttpActionResult Health() => Ok();
    }
}
