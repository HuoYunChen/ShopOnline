using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
            return _context.Order.CountAsync();
        }

        public Task<Order> FindAsync(long id)
        {
            return _context.Order.Include(x => x.Product).SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Order>> FindAsync(uint pageIndex, uint pageSize)
        {
            if (pageIndex < 1 || pageSize == 0)
            {
                return Task.Run(() => new List<Order>());
            }

            return _context.Order.Include(x => x.Product).Skip((int)((pageIndex - 1) * pageSize)).Take((int)pageSize).ToListAsync();
        }

        public Task<List<Order>> FindAsync(Expression<Func<Order, bool>> expression)
        {
            return _context.Order.Include(x => x.Product).Where(expression).ToListAsync();
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
