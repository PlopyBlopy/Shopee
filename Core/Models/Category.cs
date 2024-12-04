using System.Collections.Generic;
using Core.Interfaces;

namespace Core.Models
{
    public class Category
    {
        public const int MAX_TITLE_LENGTH = 40;
        public const int MIN_TITLE_LENGTH = 5;

        public static readonly Guid DEFAULT_CATEGORY_ID = Guid.Parse("ffa731f7-28d7-45c4-9039-029c441c54ee");

        public Guid Id { get; init; }
        public string Title { get; init; }
        public Guid ParentCategoryId { get; init; }
        public Category? ParentCategory { get; init; }
        public ICollection<Category>? Subcategories { get; init; }

        private Category(Guid id, string title, Guid parentCategoryId, Category? parentCategory = null, ICollection<Category>? subCategory = null)
        {
            Id = id;
            Title = title;
            ParentCategoryId = parentCategoryId;
            ParentCategory = parentCategory;
            Subcategories = subCategory;
        }

        public static (Category model, string error) Create(Guid id, string title, Guid parentCategoryId, Category? parentCategory = null, ICollection<Category>? subCategory = null)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title))
            {
                error = "Title cannot be empty.";
            }
            if (title.Length < MIN_TITLE_LENGTH)
            {
                error = $"Title must be at least {MIN_TITLE_LENGTH} characters long.";
            }
            if (title.Length > MAX_TITLE_LENGTH)
            {
                error = $"Title cannot exceed {MAX_TITLE_LENGTH} characters.";
            }

            var category = new Category(id, title, parentCategoryId, parentCategory, subCategory);

            return (category, error);
        }
    }
}