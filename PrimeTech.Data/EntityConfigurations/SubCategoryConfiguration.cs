using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrimeTech.Core.Models;

namespace PrimeTech.Data.EntityConfigurations
{
    public class SubCategoryConfiguration : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SubCatName).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(400);
            builder.ToTable("SubCategories");
        }
    }
}
