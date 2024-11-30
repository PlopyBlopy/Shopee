using Core.Interfaces;

namespace DataBase.Entities
{
    public class CategoryEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public Guid ParentCategoryId { get; set; }
    }
}