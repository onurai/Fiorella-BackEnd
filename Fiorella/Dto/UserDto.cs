namespace Fiorella.Dto
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }

        public int BasketId { get; set; }
    }
}
