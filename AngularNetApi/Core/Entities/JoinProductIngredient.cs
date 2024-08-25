using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Core.Entities
{
    public class JoinProductIngredient
    {
        [Key]
        public int ProductIngredientId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("IngredientId")]
        public int IngredientId { get; set; }

        // Navigation Property
        public virtual Ingredient Ingredient { get; set; }
    }
}
