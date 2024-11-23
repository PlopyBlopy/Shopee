using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Configurations;
using Persistence.Entities;

namespace Persistence.Context
{
    public class ProductsServiceDbContext : DbContext
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
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("Server=localhost;Port=5432;Database=ProductsDB;User Id=admin;Password=12345;"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new ImagesConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}