using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JakeCleary.PocketMongrels.Core
{
    public class User : IGloballyUniqueEntity
    {
        public Guid Id { get; }

        [Required(ErrorMessage = "The username is required")]
        [StringLength(12, ErrorMessage = "Username's can't be more than 12 characters long")]
        public string Name { get; set; }

        public List<Animal> Animals { get; }

        public User()
        {
            Id = Guid.NewGuid();
            Animals = new List<Animal>();
        }
    }
}