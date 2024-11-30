using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DataBase.Entities;

namespace DataBase.Configurations
{
    internal class CategoriesConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Categories").HasKey(x => x.Id);

            builder.HasIndex(x => x.Title);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(40);
            builder.HasIndex(x => x.ParentCategoryId);
            builder.Property(x => x.ParentCategoryId)
                .IsRequired();
        }
    }
}