namespace AngularNetApi.API.Models.StoreManagement
{
    public class StoreFullData
    {
        public int StoreId { get; set; }
        public string ApplicationUserId { get; set; }

        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CAP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public string? StoreTag { get; set; }
        public string? CoverImg { get; set; }
        public string? LogoImg { get; set; }
        public string? Description { get; set; }

        public ICollection<ProductFullData> Products { get; set; }
        public ICollection<WorkingDaysFullData> WorkingDays { get; set; }
        public ICollection<CategoryData> Categories { get; set; }
    }
}
