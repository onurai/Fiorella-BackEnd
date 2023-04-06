namespace Fiorella.Data.Entity
{
    public class User : BaseEntity<int>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }   

        //one to one
        public int BasketId { get; set; }
    }
}
