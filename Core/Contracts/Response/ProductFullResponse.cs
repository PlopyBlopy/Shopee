namespace Core.Contracts.Response
{
    public record ProductFullResponse(
        Guid Id,
        string Title,
        string Description,
        decimal Price,
        double Rating,
        DateTime CreateAt,
        Guid SellerId,
        Guid CategoryId);
}