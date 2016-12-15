using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Versioning;
using System.Web.Http;
using JakeCleary.PocketMongrels.Core;
using JakeCleary.PocketMongrels.Data;
using Microsoft.Web.Http;

namespace JakeCleary.PocketMongrels.Api.Controller
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/users/{userId:guid}/animals")]
    public class AnimalsController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public AnimalsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get([FromUri]Guid userId)
        {
            var user = _userRepository.ByGuid(userId);
            var animals = user.Animals.Select(Resourses.Animal.From);

            return Ok(animals);
        }

        [HttpGet]
        [Route("{animalId:guid}")]
        public IHttpActionResult Get([FromUri]Guid userId, [FromUri]Guid animalId)
        {
            // Get the user from the URI.
            var user = _userRepository.ByGuid(userId);

            if (user == null)
                return NotFound();

            // Search the user's list of animals for one with a matching guid.
            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
                return NotFound();

            return Ok(Resourses.Animal.From(animal));
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromUri]Guid userId, [FromBody]Animal animal)
        {
            var user = _userRepository.ByGuid(userId);

            if (user == null)
                return NotFound();

            if (!Enum.IsDefined(typeof(AnimalType), animal.Type))
                return BadRequest();

            user.Animals.Add(animal);

            var location = $"http://localhost/api/users/{user.Id}/animals/{animal.Id}";
            var resourse = Resourses.Animal.From(animal);

            return Created(location, resourse);
        }

        [HttpDelete]
        [Route("{animalId:guid}")]
        public IHttpActionResult Delete([FromUri]Guid userId, [FromUri]Guid animalId)
        {
            // Get the user from the URI.
            var user = _userRepository.ByGuid(userId);

            if (user == null)
                return NotFound();

            // Search the user's list of animals for one with a matching guid.
            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
                return NotFound();

            // Put the animal down.
            user.Animals.Remove(animal);

            return Ok();
        }
    }
}
