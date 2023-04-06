namespace Fiorella.Data.Entity
{
    public class Basket
    {
        
        
        public virtual ICollection<Picture> Pictures { get; set; }
        public int UserId  { get; set; }

    }
}
