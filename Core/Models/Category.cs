using Core.Interfaces;

namespace Core.Models
{
    public class Category
    {
        public const int MAX_TITLE_LENGTH = 40;
        public const int MIN_TITLE_LENGTH = 5;

        public Guid Id { get; init; }
        public string Title { get; init; }
        public Guid ParentCategoryId { get; init; }

        private Category(Guid id, string title, Guid parentCategoryId)
        {
            Id = id;
            Title = title;
            ParentCategoryId = parentCategoryId;
        }

        public static (Category model, string error) Create(Guid id, string title, Guid parentCategoryId)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title))
            {
                error = "Title cannot be empty."; //throw
            }
            if (title.Length < MIN_TITLE_LENGTH)
            {
                error = $"Title must be at least {MIN_TITLE_LENGTH} characters long.";
            }
            if (title.Length > MAX_TITLE_LENGTH)
            {
                error = $"Title cannot exceed {MAX_TITLE_LENGTH} characters.";
            }

            var category = new Category(id, title, parentCategoryId);

            return (category, error);
        }
    }
}