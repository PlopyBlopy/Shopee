using Core.Interfaces;

namespace Core.Contracts.DTO
{
    public record ProductCardDto(Guid Id, string Title, decimal Price, double Rating) : IProductDto;
}