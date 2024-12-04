using DataBase.Entities;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T model, CancellationToken ct);

        public Task AddRange(IEnumerable<T> entities, CancellationToken ct);

        public Task<T?> Get(Guid id, CancellationToken ct);

        public Task<IEnumerable<T>?> GetRange(Guid[] ids, CancellationToken ct);

        public Task<IEnumerable<T>?> GetAll(CancellationToken ct);

        public Task Update(Guid id, T entity, CancellationToken ct);

        public Task Delete(Guid id, CancellationToken ct);

        public Task DeleteRange(Guid[] ids, CancellationToken ct);
    }
}