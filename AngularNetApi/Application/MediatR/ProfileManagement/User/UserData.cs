namespace AngularNetApi.Application.MediatR.ProfileManagement.User
{
    public class UserData
    {
        public string Id { get; set; }
        public int UserProfileId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string CAP { get; set; }
        public string MobileNumber { get; set; }
    }
}
