using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JakeCleary.PocketMongrels.Messages;
using JakeCleary.PocketMongrels.Core.Entity;
using JakeCleary.PocketMongrels.Data;

namespace JakeCleary.PocketMongrels.Server.Controller
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private readonly IRepository<User> _userRepository;

        public UsersController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var users = _userRepository.All();

            var x = new List<UserResponse>();
            foreach (var user in users)
            {
                x.Add(new UserResponse
                {
                    UserId = user.Id,
                    Name = user.Name
                });
            }

            return Ok(x);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public User Get(Guid guid)
        {
            var user = _userRepository.ByGuid(guid);

            if (user == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            return user;
        }

        [HttpPost]
        [Route("")]
        public void Post([FromBody]User user)
        {
            _userRepository.Add(user);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public void Delete(Guid id)
        {
            var user =_userRepository.ByGuid(id);

            if (user == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            _userRepository.Remove(user);
        }
    }
}
