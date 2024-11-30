namespace Core.Contracts.Response
{
    public record CategoryResponse(
        Guid Id,
        string Title,
        Guid ParentCategoryId);
}