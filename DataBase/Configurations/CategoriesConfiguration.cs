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

            // Настройка отношения "многие к одному" с родительской категорией
            builder.HasOne(c => c.ParentCategory) // Указываем навигационное свойство
                .WithMany(c => c.Subcategories) // Указываем коллекцию подкатегорий
                .HasForeignKey(c => c.ParentCategoryId) // Указываем внешний ключ
                .OnDelete(DeleteBehavior.Restrict); // Настройка поведения при удалении
        }
    }
}