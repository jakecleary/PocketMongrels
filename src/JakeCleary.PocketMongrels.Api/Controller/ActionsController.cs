using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using JakeCleary.PocketMongrels.Services;

namespace JakeCleary.PocketMongrels.Api.Controller
{
    [RoutePrefix("api/users/{userId:guid}/animals/{animalId:guid}")]
    public class ActionsController : ApiController
    {
        private readonly UserService _userService;
        private readonly AnimalService _animalService;

        public ActionsController(UserService userService, AnimalService animalService)
        {
            _userService = userService;
            _animalService = animalService;
        }
        
        [HttpPost]
        [Route("feed")]
        public IHttpActionResult Feed([FromUri] Guid userId, [FromUri] Guid animalId)
        {
            var user = _userService.FindById(userId);

            if (user == null)
                return NotFound();

            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
                return NotFound();

            _animalService.Feed(animal);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("pet")]
        public IHttpActionResult Pet([FromUri] Guid userId, [FromUri] Guid animalId)
        {
            var user = _userService.FindById(userId);

            if (user == null)
                return NotFound();

            var animal = user.Animals.FirstOrDefault(a => a.Id == animalId);

            if (animal == null)
                return NotFound();

            _animalService.Pet(animal);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
