using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Entities
{
    public abstract class ProfileBase
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserCredentials")]
        public string UserCredentialsId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
