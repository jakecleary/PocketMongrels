using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JakeCleary.PocketMongrels.Core.Entity;
using JakeCleary.PocketMongrels.Data;

namespace JakeCleary.PocketMongrels.Server.Controller
{
    [RoutePrefix("api/users/{userId:guid}/animals")]
    public class AnimalsController : ApiController
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Animal> _animalRepository;

        public AnimalsController(IRepository<User> userRepository, IRepository<Animal> animalRepository)
        {
            _userRepository = userRepository;
            _animalRepository = animalRepository;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<Animal> Get([FromUri]Guid userId)
        {
            var user = _userRepository.ByGuid(userId);

            return user.Animals;
        }

        [HttpGet]
        [Route("{animalId:guid}")]
        public Animal Get([FromUri]Guid userId, [FromUri]Guid animalId)
        {
            // Get the user from the URI.
            var user = _userRepository.ByGuid(userId);

            if (user == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            // Search the user's list of animals for one with a matching guid.
            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Animal not found.")
                };

                throw new HttpResponseException(response);
            }

            return animal;
        }

        [HttpPost]
        [Route("")]
        public void Post([FromUri]Guid userId, [FromBody]Animal animal)
        {
            var user = _userRepository.ByGuid(userId);

            user.Animals.Add(animal);
        }

        [HttpDelete]
        [Route("{animalId:guid}")]
        public void Delete([FromUri]Guid userId, [FromUri]Guid animalId)
        {
            // Get the user from the URI.
            var user = _userRepository.ByGuid(userId);

            if (user == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            // Search the user's list of animals for one with a matching guid.
            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Animal not found.")
                };

                throw new HttpResponseException(response);
            }

            // Put the animal down.
            user.Animals.Remove(animal);
        }
    }
}
