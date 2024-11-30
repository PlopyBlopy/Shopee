using System;

namespace Core.Models
{
    public class Image
    {
        public const int MAX_PATH_LENGTH = 260;

        public Guid Id { get; }
        public int Order { get; }
        public string Path { get; }
        public Guid ProductId { get; }

        private Image(Guid id, int order, string path, Guid productId)
        {
            Id = id;
            Order = order;
            Path = path;
            ProductId = productId;
        }

        public static (Image model, string error) Create(Guid id, int order, string path, Guid productId)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(path))
            {
                error = "Path cannot be empty.";
            }
            if (path.Length > MAX_PATH_LENGTH)
            {
                error = $"Path length cannot exceed {MAX_PATH_LENGTH} characters.";
            }

            var image = new Image(id, order, path, productId);

            return (image, error);
        }
    }
}