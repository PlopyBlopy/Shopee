namespace Core.Models
{
    public class Category
    {
        public const int MAX_TITLE_LENGTH = 40;

        public int Id { get; }
        public string Title { get; }
        public int ParentCategoryId { get; }
        public ICollection<Product> Products { get; }

        private Category(string title, int parentCategoryId, ICollection<Product> products)
        {
            Title = title;
            ParentCategoryId = parentCategoryId;
            Products = products;
        }

        public static (Category category, string error) Create(string title, int parentCategoryId, ICollection<Product> products)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title is required and cannot exceed " + MAX_TITLE_LENGTH + " characters.";
            }

            if (parentCategoryId < 0)
            {
                error = "Parent category ID must be a non-negative value.";
            }

            var category = new Category(title, parentCategoryId, products);

            return (category, error);
        }
    }
}