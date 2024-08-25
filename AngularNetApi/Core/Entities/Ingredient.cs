using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace AngularNetApi.Core.Entities
{
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        public int StoreId { get; set; }

        [Required]
        public string IngredientName { get; set; }

        [Required]
        [Precision(18, 2)]
        public decimal IngredientPrice { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}
