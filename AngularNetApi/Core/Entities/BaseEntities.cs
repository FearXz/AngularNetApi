using System.ComponentModel.DataAnnotations;

namespace AngularNetApi.Core.Entities
{
    public abstract class BaseEntities
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
