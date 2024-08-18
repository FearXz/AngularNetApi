using System.ComponentModel.DataAnnotations;

namespace AngularNetApi.Core.Entities
{
    public class UserProfile : ProfileBase
    {
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
        public string MobileNumber { get; set; }

        // Navigation property

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
