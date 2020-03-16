using PrimeTech.Core.Models;
using PrimeTech.Core.Repositories;

namespace PrimeTech.Data.Repositories
{
    public class SubCategoryRepository : Repository<SubCategory>, ISubCategoryRepository
    {
        public SubCategoryRepository(PrimeTechDbContext primeTechDbContext)
            : base(primeTechDbContext)
        {

        }

        public PrimeTechDbContext PrimeTechDbContext { get { return Context as PrimeTechDbContext; } }
    }
}
