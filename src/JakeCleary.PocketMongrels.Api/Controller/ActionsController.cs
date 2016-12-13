using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JakeCleary.PocketMongrels.Core;
using JakeCleary.PocketMongrels.Data;
using JakeCleary.PocketMongrels.Services;

namespace JakeCleary.PocketMongrels.Api.Controller
{
    [RoutePrefix("api/users/{userId:guid}/animals/{animalId:guid}")]
    public class ActionsController : ApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly AnimalService _animalService;

        public ActionsController(IUserRepository userRepository, AnimalService animalService)
        {
            _userRepository = userRepository;
            _animalService = animalService;
        }
        
        [HttpPost]
        [Route("feed")]
        public IHttpActionResult Feed([FromUri] Guid userId, [FromUri] Guid animalId)
        {
            // Get the animal.
            var user = _userRepository.ByGuid(userId);

            if (user == null)
            {
                // Send a 404 response if the user doesn't exist.
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
            {
                // Send a 404 response if the animal doesn't exist.
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Animal not found.")
                };

                throw new HttpResponseException(response);
            }

            _animalService.Feed(animal);
            
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

            if (user == null)
            {
                // Send a 404 response if the user doesn't exist.
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found.")
                };

                throw new HttpResponseException(response);
            }

            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
            {
                // Send a 404 response if the animal doesn't exist.
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("Animal not found.")
                };

                throw new HttpResponseException(response);
            }

            _animalService.Pet(animal);

            // Return the full animal object back.
            var successResponse = Request.CreateResponse(HttpStatusCode.OK, animal);
            successResponse.Headers.Location = new Uri($"http://localhost:53460/api/users/{userId}/animals/{animalId}");
            return successResponse;
        }
    }
}
