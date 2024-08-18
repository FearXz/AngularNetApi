namespace AngularNetApi.DTOs.User
{
    public class CreateUserResponse
    {
        public string NewUserId { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
