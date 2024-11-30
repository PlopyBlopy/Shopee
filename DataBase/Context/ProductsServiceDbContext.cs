using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DataBase.Configurations;
using DataBase.Entities;
using Core.Interfaces;

namespace DataBase.Context
{
    public class ProductsServiceDbContext : DbContext, IProductsServiceDbContext
    {
        public IConfiguration Configuration { get; init; }

        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<ImageEntity> Images { get; set; }

        public ProductsServiceDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString(nameof(ProductsServiceDbContext)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new ImagesConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}