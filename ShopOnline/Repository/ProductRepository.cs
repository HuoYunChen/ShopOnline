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
    public class ProductRepository : IRepository<Product>
    {
        private readonly ShopOnlineContext _context;

        public ProductRepository(ShopOnlineContext context)
        {
            _context = context;
        }

        public Task<int> CountAsync()
        {
            return _context.Product.CountAsync();
        }

        public Task<Product> FindAsync(long id)
        {
            return _context.Product.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Task<List<Product>> FindAsync(uint pageIndex, uint pageSize)
        {
            if (pageIndex < 1 || pageSize <= 0)
            {
                return Task.Run(() => new List<Order>());
            }

            return _context.Product.Skip((int) ((pageIndex - 1) * pageSize)).Take((int) pageSize).ToListAsync();
        }

        public Task<List<Product>> FindAsync(Expression<Func<Product, bool>> expression)
        {
            return _context.Product.Where(expression).ToListAsync();
        }

        public async Task UpdateAsync(IEnumerable<Product> entities)
        {
            if (entities == null || entities.Count() == 0)
            {
                return;
            }

            _context.Product.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
    }
}
