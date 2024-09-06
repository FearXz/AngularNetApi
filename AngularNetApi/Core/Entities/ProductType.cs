using System.ComponentModel.DataAnnotations;

namespace AngularNetApi.Core.Entities
{
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }

        [Required]
        public string ProductTypeName { get; set; }
    }
}
