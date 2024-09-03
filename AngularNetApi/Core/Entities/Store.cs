using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AngularNetApi.Core.Entities.Join;

namespace AngularNetApi.Core.Entities
{
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }

        [Required]
        public string StoreName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [StringLength(5)]
        public string CAP { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // NULLABLE
        public string? StoreTag { get; set; }
        public string? CoverImg { get; set; }
        public string? LogoImg { get; set; }
        public string? Description { get; set; }

        // Navigation Property

        public virtual FiscalData FiscalData { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<JoinStoreCategory> JoinStoreCategory { get; set; }
        public virtual ICollection<JoinStoreWeekDay> JoinStoreOpeningDay { get; set; }
    }
}
