using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products").HasKey(x => x.Id);

            builder.HasIndex(x => x.Title);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(700);

            builder.HasIndex(x => x.Price);
            builder.Property(x => x.Price)
                .IsRequired();

            builder.HasIndex(x => x.Rating);
            builder.Property(x => x.Rating)
                .IsRequired();

            builder.Property(x => x.SellerId)
                .IsRequired();

            builder.HasIndex(x => x.CreateAt);
            builder.Property(x => x.CreateAt)
                .IsRequired();

            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.HasMany(p => p.ImageEntities)
                .WithOne(i => i.ProductEntity)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}