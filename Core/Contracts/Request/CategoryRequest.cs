namespace Core.Contracts.Request
{
    public record CategoryRequest(
        string Title,
        Guid ParentCategoryId);
}