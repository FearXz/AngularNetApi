namespace AngularNetApi.API.Models.StoreManagement
{
    public class CreateStoreRequest
    {
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CAP { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string PhoneNumber { get; set; }
    }
}
