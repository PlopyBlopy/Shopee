namespace Core.Contracts.Request
{
    public record ProductFiltersRequest(string? Search, string? SortItem, string? SortOrder);
}