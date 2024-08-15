namespace AngularNetApi.Factory.Interfaces
{
    public interface ICompanyClaims : IBaseClaims
    {
        string CompanyName { get; }
        string VatNumber { get; }
        string Address { get; }
        string City { get; }
        string Cap { get; }
        string Email { get; }
        string PhoneNumber { get; }
        string TelephoneNumber { get; }
    }
}
