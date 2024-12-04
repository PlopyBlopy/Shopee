using DataBase.Entities;

namespace Core.Interfaces
{
    public interface ICategoriesRepository : IRepository<CategoryEntity>
    {
        public Task<CategoryEntity> GetCategoryTree(CancellationToken ct);
    }
}