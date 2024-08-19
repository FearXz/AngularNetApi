﻿using System.ComponentModel.DataAnnotations;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Domain.Entities
{
    public class CompanyProfile : ProfileBase
    {
        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string VATNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string CAP { get; set; }

        [Required]
        public string TelephoneNumber { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        // Navigation property
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}