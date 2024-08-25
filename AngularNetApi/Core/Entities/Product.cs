using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Core.Entities
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal ProductPrice { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        public string? Description { get; set; }
        public string? ProductImg { get; set; }

        // Navigation Property
        public virtual ICollection<JoinProductIngredient> JoinProductIngredient { get; set; }
    }
}
