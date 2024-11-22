namespace Persistence.Entities
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Rating { get; set; }
        public int SellerId { get; set; }
        public DateTime CreateAt { get; set; }
        public int CategoryId { get; set; }
        public CategoryEntity CategoryEntity { get; set; }
        public ICollection<ImageEntity>? ImageEntities { get; set; }
    }
}