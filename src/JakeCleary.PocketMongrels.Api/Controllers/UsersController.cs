using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JakeCleary.PocketMongrels.Core.Entity;
using JakeCleary.PocketMongrels.Data.Repository;

namespace JakeCleary.PocketMongrels.Api.Controllers
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
        public IEnumerable<User> Get()
        {
            return _userRepository.All();
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
