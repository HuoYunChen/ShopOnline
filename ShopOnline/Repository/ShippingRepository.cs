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
    public class ShippingRepository : IRepository<Shipping>
    {
        private readonly ShopOnlineContext _context;

        public ShippingRepository(ShopOnlineContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope()) 
            {
                return await _context.Shipping.CountAsync();
            }
        }

        public Task CreateAsync(IEnumerable<Shipping> entities)
        {
            if (entities == null || entities.Count() == 0)
            {
                return null;
            }

            _context.Shipping.AddRange(entities);
            return _context.SaveChangesAsync();
        }

        public async Task<Shipping> FindAsync(long id)
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Shipping.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<List<Shipping>> FindAsync(uint pageIndex, uint pageSize)
        {
            if (pageIndex < 1 || pageSize <= 0)
            {
                return new List<Shipping>();
            }

            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Shipping.AsNoTracking().Skip((int)((pageIndex - 1) * pageSize)).Take((int)pageSize).ToListAsync();
            }

        }

        public async Task<List<Shipping>> FindAsync(Expression<Func<Shipping, bool>> expression)
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Shipping.AsNoTracking().Where(expression).ToListAsync();
            }
        }

        public async Task UpdateAsync(IEnumerable<Shipping> entities)
        {
            if (entities == null || entities.Count() == 0)
            {
                return;
            }

            _context.Shipping.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
