using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Persistence.Entities;

namespace Persistence.Configurations
{
    internal class ImagesConfiguration : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.ToTable("Images").HasKey(x => x.Id);

            builder.Property(x => x.Path)
                .IsRequired()
                .HasMaxLength(260);

            builder.Property(x => x.ProductId)
                .IsRequired();
        }
    }
}