namespace Core.Contracts.DTO
{
    public record CategoryReadAllDto(Guid Id, string Title, Guid ParentCategoryId, ICollection<CategoryReadAllDto>? Subcategories);
}