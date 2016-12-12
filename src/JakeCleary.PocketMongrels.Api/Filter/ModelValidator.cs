using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace JakeCleary.PocketMongrels.Api.Filter
{
    class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            if (context.ModelState.IsValid == false)
            {
                context.Response = context.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, context.ModelState
                );
            }
        }
    }
}
