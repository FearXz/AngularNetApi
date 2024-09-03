using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Core.Entities
{
    public class JoinStoreCategory
    {
        [Key]
        public int JoinStoreCategoryId { get; set; }

        [Required]
        [ForeignKey("Store")]
        public int StoreId { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        // NAVIGATION PROPERTY

        public virtual Category Category { get; set; }
    }
}
