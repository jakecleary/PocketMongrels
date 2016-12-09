﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JakeCleary.PocketMongrels.Core;
using JakeCleary.PocketMongrels.Data;

namespace JakeCleary.PocketMongrels.Api.Controller
{
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
                // Send a 404 response if the user doesn't exist.
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
                // Send a 404 response if the animal doesn't exist.
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
        public IHttpActionResult Create([FromUri]Guid userId, [FromBody]Animal animal)
        {
            var user = _userRepository.ByGuid(userId);

            user.Animals.Add(animal);

            var location = $"http://localhost/api/users/{user.Id}/animals/{animal.Id}";
            var resourse = Resourses.Animal.From(animal);

            return Created(location, resourse);
        }

        [HttpDelete]
        [Route("{animalId:guid}")]
        public void Delete([FromUri]Guid userId, [FromUri]Guid animalId)
        {
            // Get the user from the URI.
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

            // Search the user's list of animals for one with a matching guid.
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

            // Put the animal down.
            user.Animals.Remove(animal);
        }
    }
}
