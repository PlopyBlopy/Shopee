namespace Core.Models
{
    public class Product
    {
        public const int MAX_TITLE_LENGTH = 100;
        public const int MIN_TITLE_LENGTH = 5;

        public const int MAX_DESCRIPTION_LENGTH = 700;

        public const decimal MIN_PRICE = 50;
        public const decimal MAX_PRICE = 1000000;

        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
        public decimal Price { get; }
        public double Rating { get; }
        public DateTime CreateAt { get; }
        public Guid SellerId { get; }
        public Guid CategoryId { get; }

        private Product(Guid id, string title, string description, decimal price, double rating, DateTime createAt, Guid sellerId, Guid categoryId)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            Rating = rating;
            CreateAt = createAt;
            SellerId = sellerId;
            CategoryId = categoryId;
        }

        public static (Product model, string error) Create(Guid id, string title, string description, decimal price, double rating, DateTime createAt, Guid sellerId, Guid categoryId)
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

            if (description.Length > MAX_DESCRIPTION_LENGTH)
            {
                error = $"Description must not exceed the maximum length of {MAX_DESCRIPTION_LENGTH} characters.";
            }

            if (price < MIN_PRICE)
            {
                error = $"Price must be at least {MIN_PRICE}.";
            }
            if (price < MAX_PRICE)
            {
                error = $"Price must be at exceed {MAX_PRICE}.";
            }

            if (rating < 0 || rating > 5)
            {
                error = "Rating must be between 0 and 5.";
            }

            var product = new Product(id, title, description, price, rating, createAt, sellerId, categoryId);

            return (product, error);
        }
    }
}