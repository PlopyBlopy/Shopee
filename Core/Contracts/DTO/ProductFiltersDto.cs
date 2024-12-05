namespace Core.Contracts.DTO
{
    public record ProductFiltersDto(string? Search, Guid? CategoryId, string? SortProp, string? SortOrder);
}