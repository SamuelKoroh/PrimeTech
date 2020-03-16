using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeTech.Core.Models;

namespace PrimeTech.Data.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CatName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(400);
            builder.ToTable("Categories");
        }
    }
}
