namespace Fiorella.Data.Entity
{
    public class Picture : BaseEntity<int>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public int Price { get; set; }
        public string Source { get; set; }

        public virtual ICollection<Picture>? Pictures { get; set; }
    }
}
