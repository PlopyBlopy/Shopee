using Microsoft.EntityFrameworkCore;
using DataBase.Entities;

namespace Core.Interfaces
{
    public interface IProductsServiceDbContext : IDbContext
    {
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
    }
}