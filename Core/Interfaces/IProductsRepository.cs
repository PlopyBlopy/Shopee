using Core.Contracts.DTO;
using DataBase.Entities;

namespace Core.Interfaces
{
    public interface IProductsRepository : IRepository<ProductEntity>
    {
        public Task<IEnumerable<ProductEntity>?> ReadCategoryAll(Guid categoryId, CancellationToken ct);

        public Task<IEnumerable<ProductCardDto>?> ReadCardFiltered(ProductFiltersDto filter, CancellationToken ct);
    }
}