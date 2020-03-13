using PrimeTech.Core;
using PrimeTech.Core.Repositories;
using PrimeTech.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrimeTech.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PrimeTechDbContext _primeTechDbContext;
        public UnitOfWork(PrimeTechDbContext primeTechDbContext)
        {
            _primeTechDbContext = primeTechDbContext;
            Categories = new CategoryRepository(_primeTechDbContext);
        }

        public ICategoryRepository Categories  { get; private set; }
        public ISubCategoryRepository SubCategories { get; private set; }

        public int CommitChanges()
        {
            return _primeTechDbContext.SaveChanges();
        }

        public async Task<int> CommitChangesAsync()
        {
            return await _primeTechDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _primeTechDbContext.DisposeAsync();
        }
    }
}
