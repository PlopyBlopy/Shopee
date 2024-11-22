namespace Persistence.Entities
{
    public class ImageEntity
    {
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public ProductEntity ProductEntity { get; set; }
    }
}