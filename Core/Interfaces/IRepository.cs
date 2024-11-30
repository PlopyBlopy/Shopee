using DataBase.Entities;

namespace Core.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T model, CancellationToken ct);

        public Task Add(IEnumerable<T> entities, CancellationToken ct);

        public Task<T?> Read(Guid id, CancellationToken ct);

        public Task<IEnumerable<T>?> Read(Guid[] ids, CancellationToken ct);

        public Task<IEnumerable<T>?> ReadAll(CancellationToken ct);

        public Task Update(Guid id, T entity, CancellationToken ct);

        public Task Delete(Guid id, CancellationToken ct);

        public Task Delete(Guid[] ids, CancellationToken ct);
    }
}