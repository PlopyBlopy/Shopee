using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Context
{
    public interface IProductsServiceDbContext : IDbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
    }
}