using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Entities;

namespace Persistence.Context
{
    public interface IProductsServiceDbContext : IDbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Category { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
    }
}