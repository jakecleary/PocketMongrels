using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JakeCleary.PocketMongrels.Core.Entity;
using JakeCleary.PocketMongrels.Data;

namespace JakeCleary.PocketMongrels.Server.Controller
{
    [RoutePrefix("api/users/{userId:guid}/animals/{animalId:guid}")]
    public class ActionsController : ApiController
    {
        private readonly IRepository<User> _userRepository;

        public ActionsController(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        
        [HttpPost]
        [Route("feed")]
        public IHttpActionResult Feed([FromUri] Guid userId, [FromUri] Guid animalId)
        {
            // Get the animal.
            var user = _userRepository.ByGuid(userId);
            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
            {
                var notFoundResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Animal not found.")
                };

                throw new HttpResponseException(notFoundResponse);
            }

            // Feed the animal some tasty treats.
            animal.Hunger = Animal.MinScore;
            animal.LastFeed = DateTime.UtcNow;
            
            return Ok(animal);

            // Return the full animal object back.
            //var successResponse = Request.CreateResponse(HttpStatusCode.OK, );
            //successResponse.Headers.Location = new Uri($"http://localhost:53460/api/users/{userId}/animals/{animalId}");
            //return successResponse;
        }

        [HttpPost]
        [Route("pet")]
        public HttpResponseMessage Pet([FromUri] Guid userId, [FromUri] Guid animalId)
        {
            // Get the animal.
            var user = _userRepository.ByGuid(userId);
            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
            {
                var notFoundResponse = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Animal not found.")
                };

                throw new HttpResponseException(notFoundResponse);
            }

            // Give the animal a cuddle.
            animal.Happiness = Animal.MaxScore;
            animal.LastPet = DateTime.UtcNow;

            // Return the full animal object back.
            var successResponse = Request.CreateResponse(HttpStatusCode.OK, animal);
            successResponse.Headers.Location = new Uri($"http://localhost:53460/api/users/{userId}/animals/{animalId}");
            return successResponse;
        }
    }
}
