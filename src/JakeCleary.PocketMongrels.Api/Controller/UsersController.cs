using System;
using System.Web.Http;
using JakeCleary.PocketMongrels.Api.Filter;
using JakeCleary.PocketMongrels.Services;
using Microsoft.Web.Http;

namespace JakeCleary.PocketMongrels.Api.Controller
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var users = _userService.All();
            users.ForEach(u => Resourses.User.From(u));

            return Ok(users);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IHttpActionResult Get(Guid id)
        {
            var user = _userService.FindById(id);

            if (user == null)
                return NotFound();

            return Ok(Resourses.User.From(user));
        }

        [HttpPost]
        [Route("")]
        [ValidateModel]
        public IHttpActionResult Post([FromBody]Core.User user)
        {
            _userService.Add(user);

            var url = $"http://localhost/api/users/{user.Id}";
            var resource = Resourses.User.From(user);

            return Created(url, resource);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IHttpActionResult Delete(Guid id)
        {
            var user = _userService.FindById(id);

            if (user == null)
                return NotFound();

            _userService.Remove(user);

            return Ok();
        }
    }
}
