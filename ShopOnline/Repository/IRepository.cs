using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ShopOnline.Repository
{
    public interface IRepository<TEntity>
    {
        Task CreateAsync(IEnumerable<TEntity> entities);

        Task UpdateAsync(IEnumerable<TEntity> entities);

        Task<TEntity> FindAsync(long id);

        Task<List<TEntity>> FindAsync(uint pageIndex, uint pageSize);

        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> expression);

        Task<int> CountAsync();
    }
}
