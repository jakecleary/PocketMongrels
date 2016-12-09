using System;
using System.Web.Http;
using JakeCleary.PocketMongrels.Data;
using JakeCleary.PocketMongrels.Services;

namespace JakeCleary.PocketMongrels.Api.Controller
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly UserService _userService;

        public UsersController(IUserRepository userRepository, UserService userService)
        {
            _userRepository = userRepository;
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
            {
                return NotFound();
            }

            return Ok(Resourses.User.From(user));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody]Core.User user)
        {
            _userRepository.Add(user);

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
            {
                return NotFound();
            }

            _userRepository.Remove(user);

            return Ok();
        }
    }
}
