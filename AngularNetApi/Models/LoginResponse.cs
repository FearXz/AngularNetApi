namespace AngularNetApi.Models
{
    public class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
        public Guid IdUtente { get; set; } = Guid.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Citta { get; set; } = string.Empty;
        public string Cellulare { get; set; } = string.Empty;
        public string Indirizzo { get; set; } = string.Empty;
        public string CAP { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
