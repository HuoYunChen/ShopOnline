using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Transactions;

namespace ShopOnline.Repository
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly ShopOnlineContext _context;

        public OrderRepository(ShopOnlineContext context)
        {
            _context = context;
        }

        public Task<int> CountAsync()
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return _context.Order.AsNoTracking().CountAsync();
            }
        }

        public async Task CreateAsync(IEnumerable<Order> entities)
        {
            if (entities == null || entities.Count() == 0)
            {
                return;
            }

            _context.Order.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<Order> FindAsync(long id)
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Order.AsNoTracking().Include(x => x.Product).SingleOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<List<Order>> FindAsync(uint pageIndex, uint pageSize)
        {
            if (pageIndex < 1 || pageSize == 0)
            {
                return new List<Order>();
            }

            using(TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Order.AsNoTracking().Include(x => x.Product).Skip((int)((pageIndex - 1) * pageSize)).Take((int) pageSize).ToListAsync();
            }
        }

        public async Task<List<Order>> FindAsync(Expression<Func<Order, bool>> expression)
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Order.AsNoTracking().Include(x => x.Product).Where(expression).ToListAsync();
            }
        }

        public async Task UpdateAsync(IEnumerable<Order> entities)
        {
            if (entities == null || entities.Count() == 0)
            {
                return;
            }

            _context.Order.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
