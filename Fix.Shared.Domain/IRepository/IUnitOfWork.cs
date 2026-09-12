using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Fix.Shared.Domain.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        IBaseRepository<TEntity> BaseRepository<TEntity>() where TEntity : class;
        IDbContextTransaction BeginTransaction();
        Task<int> SaveChangesAsync();


    }
}
