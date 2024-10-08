﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Core.Entities
{
    public class UserProfile : BaseEntities
    {
        [Key]
        public int UserProfileId { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string CAP { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        // Navigation Property

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
