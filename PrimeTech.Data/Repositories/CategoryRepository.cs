using Microsoft.EntityFrameworkCore;
using PrimeTech.Core.Models;
using PrimeTech.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrimeTech.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(PrimeTechDbContext context) : base(context)
        {
        }


        public PrimeTechDbContext PrimeTechDbContext { get { return Context as PrimeTechDbContext; } }
    }
}
