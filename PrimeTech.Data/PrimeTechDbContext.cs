using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using PrimeTech.Core.Models;
using Microsoft.EntityFrameworkCore;
using PrimeTech.Data.EntityConfigurations;

namespace PrimeTech.Data
{
    public class PrimeTechDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public PrimeTechDbContext(DbContextOptions<PrimeTechDbContext> options)
            : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new SubCategoryConfiguration());
            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
