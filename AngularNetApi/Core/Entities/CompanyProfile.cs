using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AngularNetApi.Core.Entities;

namespace AngularNetApi.Domain.Entities
{
    public class CompanyProfile : BaseEntities
    {
        [Key]
        public int CompanyProfileId { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

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

        // Navigation Property

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
