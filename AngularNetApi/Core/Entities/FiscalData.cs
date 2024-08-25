using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AngularNetApi.Core.Entities
{
    public class FiscalData
    {
        [Key]
        public int FiscalDataId { get; set; }

        [Required]
        [ForeignKey("StoreId")]
        public int StoreId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public string VATNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        [StringLength(5)]
        public string CAP { get; set; }

        [Required]
        public string IBAN { get; set; }

        [Required]
        [StringLength(6)]
        public string UniqueCode { get; set; }
    }
}
