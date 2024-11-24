using Microsoft.Extensions.Configuration;

namespace Persistence.Context
{
    public interface IDbContext
    {
        public IConfiguration Configuration { get; init; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}