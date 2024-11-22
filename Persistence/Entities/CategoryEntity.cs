namespace Persistence.Entities
{
    public class CategoryEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int ParentCategoryId { get; set; }
        public ICollection<ProductEntity>? ProductsEntities { get; set; }
    }
}