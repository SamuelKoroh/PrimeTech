using PrimeTech.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace PrimeTech.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        ISubCategoryRepository SubCategories { get; }
        Task<int> CommitChangesAsync();
        int CommitChanges();
    }
}
