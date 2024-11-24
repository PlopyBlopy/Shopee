using Persistence.Context;

namespace Persistence.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task Add(T entity, CancellationToken ct);

        public Task Add(IEnumerable<T> entities, CancellationToken ct);

        public Task Update(int id, Action<T> entity, CancellationToken ct);

        public Task<T?> Read(int id, CancellationToken ct);

        public Task<IEnumerable<T>?> Read(int[] ids, CancellationToken ct);

        public Task Delete(int id, CancellationToken ct);
    }
}