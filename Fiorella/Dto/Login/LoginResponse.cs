namespace Fiorella.Dto.Login
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string? Token { get; set; }
        public string Firstname { get; set; }
    }
}
