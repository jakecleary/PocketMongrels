using System;
using System.Collections.Generic;
using System.Linq;

namespace JakeCleary.PocketMongrels.Api.Resourses
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Animal> Animals { get; set; } = new List<Animal>();

        public static User From(Core.Entity.User user)
        {
            return new User()
            {
                Id = user.Id,
                Name = user.Name,
                Animals = user.Animals.Select(a => Animal.From(a)).ToList()
            };
        }
    }
}
