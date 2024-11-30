namespace DataBase.Entities
{
    public class ImageEntity
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
        public string Path { get; set; } = string.Empty;
        public Guid ProductId { get; set; }
    }
}