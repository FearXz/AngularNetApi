using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Entities
{
    public class CompanyProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserCredentials")]
        public string UserCredentialsId { get; set; }

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

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation property
        public virtual UserCredentials UserCredentials { get; set; }
    }
}
