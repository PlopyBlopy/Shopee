using Core.Interfaces;

namespace Core.Contracts.Response
{
    public record ProductFilteredResponse(IEnumerable<IProductDto> ProductDtos);
}