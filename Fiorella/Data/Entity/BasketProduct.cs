namespace Fiorella.Data.Entity
{
    public class BasketProduct : BaseEntity<int>
    {
        public int BasketId { get; set; }
        public int PictureId { get; set; }

        public Basket Basket { get; set; }
        public Picture Picture { get; set; }
    }
}
