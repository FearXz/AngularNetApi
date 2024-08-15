namespace AngularNetApi.Factory.Interfaces
{
    public interface IUserClaims : IBaseClaims
    {
        string Name { get; }
        string Surname { get; }
        string Email { get; }
        string Address { get; }
        string City { get; }
        string Cap { get; }
        string MobileNumber { get; }
    }
}
