namespace Core.Contracts.Request
{
    public record ProductCreateRequest(
        string Title,
        string Description,
        decimal Price,
        double Rating,
        Guid SellerId,
        Guid CategoryId);
}