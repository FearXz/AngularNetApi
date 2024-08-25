using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace AngularNetApi.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        // Navigation properties
        [InverseProperty("ApplicationUser")]
        public virtual UserProfile UserProfile { get; set; }

        public virtual Store Store { get; set; }
    }
}
