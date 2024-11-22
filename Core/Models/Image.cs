using System;

namespace Core.Models
{
    public class Image
    {
        public const int MAX_PATH_LENGTH = 260;

        public int Id { get; }
        public string Path { get; }
        public int ProductId { get; }

        private Image(string path, int productId)
        {
            Path = path;
            ProductId = productId;
        }

        public (Image image, string error) Create(string path, int productId)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(path) || path.Length > MAX_PATH_LENGTH)
            {
                error = "Path is required and cannot exceed " + MAX_PATH_LENGTH + " characters.";
            }

            if (productId < 0)
            {
                error = "Product ID must be a non-negative value.";
            }

            var image = new Image(path, productId);

            return (image, error);
        }
    }
}