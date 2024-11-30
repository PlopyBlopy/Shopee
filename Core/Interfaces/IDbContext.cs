using Microsoft.Extensions.Configuration;

namespace Core.Interfaces
{
    public interface IDbContext
    {
        public IConfiguration Configuration { get; init; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}