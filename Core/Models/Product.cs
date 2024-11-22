namespace Core.Models
{
    public class Product
    {
        public const int MAX_TITLE_LENGTH = 200;
        public const int MAX_DESCRIPTION_LENGTH = 700;

        public int Id { get; }
        public string Title { get; }
        public string Description { get; }
        public decimal price { get; }
        public double Rating { get; }
        public int SellerId { get; }
        public DateTime CreateAt { get; }
        public Category Category { get; }
        public ICollection<Image> ProductImages { get; }

        private Product(string title, string description, decimal price, double rating, int sellerId)
        {
            Title = title;
            Description = description;
            Rating = rating;
            SellerId = sellerId;
            CreateAt = DateTime.UtcNow;
        }

        public static (Product product, string error) Create(string title, string description, decimal price, double rating, int sellerId)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title cannot be empty and must not exceed the maximum length of " + MAX_TITLE_LENGTH + " characters.";
            }

            if (description.Length > MAX_DESCRIPTION_LENGTH)
            {
                error = "Description must not exceed the maximum length of " + MAX_DESCRIPTION_LENGTH + " characters.";
            }

            if (price < 0)
            {
                error = "Price cannot be negative.";
            }

            if (rating < 0 || rating > 5)
            {
                error = "Rating must be between 0 and 5.";
            }

            if (sellerId < 0)
            {
                error = "Seller ID cannot be negative.";
            }

            var product = new Product(title, description, price, rating, sellerId);

            return (product, error);
        }
    }
}