namespace Core.Contracts.Request
{
    public record ProductFiltersRequest(string? Search, string? CategoryId, string? SortItem, string? SortOrder);
}