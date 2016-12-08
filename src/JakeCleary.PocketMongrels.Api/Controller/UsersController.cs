using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using JakeCleary.PocketMongrels.Data;
using JakeCleary.PocketMongrels.Api.Resourses;

namespace JakeCleary.PocketMongrels.Api.Controller
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Core.Entity.User> Get()
        {
            return _userRepository.All();
        }

        [HttpGet]
        [Route("{id:guid}")]
        public User Get(Guid id)
        {
            // Find the user.
            var user = _userRepository.ByGuid(id);

            if (user == null)
            {
                // Send a 404 response if the user doesn't exist.
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            // Return the user as an API resource.
            return Resourses.User.From(user);
        }

        [HttpPost]
        [Route("")]
        public CreatedNegotiatedContentResult<User> Post([FromBody]Core.Entity.User user)
        {
            // Store the user.
            _userRepository.Add(user);

            // Send back the new API resourse, with a pointer to where it is located.
            return Created($"/api/users/{user.Id}", Resourses.User.From(user));
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public void Delete(Guid id)
        {
            // Find the user.
            var user =_userRepository.ByGuid(id);

            if (user == null)
            {
                // Send a 404 response if the user doesn't exist.
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            // Delete the user.
            _userRepository.Remove(user);
        }
    }
}
