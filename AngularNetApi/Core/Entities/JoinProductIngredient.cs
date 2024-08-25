using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Core.Entities
{
    public class JoinProductIngredient
    {
        [Key]
        public int JoinProductIngredientId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [Required]
        [ForeignKey("IngredientId")]
        public int IngredientId { get; set; }

        // Proprietà di navigazione
        public virtual Ingredient Ingredient { get; set; }
    }
}
