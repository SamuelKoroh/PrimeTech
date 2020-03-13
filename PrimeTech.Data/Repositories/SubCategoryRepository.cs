using PrimeTech.Core.Models;
using PrimeTech.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
