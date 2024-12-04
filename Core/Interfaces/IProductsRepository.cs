using Core.Contracts.DTO;
using DataBase.Entities;

namespace Core.Interfaces
{
    public interface IProductsRepository : IRepository<ProductEntity>
    {
        public Task<IEnumerable<ProductEntity>?> GetCategoryAll(Guid categoryId, CancellationToken ct);

        public Task<IEnumerable<ProductCardDto>?> GetFiltered(ProductFiltersDto filter, CancellationToken ct);
    }
}