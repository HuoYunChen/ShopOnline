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
    public class ProductRepository : IRepository<Product>
    {
        private readonly ShopOnlineContext _context;

        public ProductRepository(ShopOnlineContext context)
        {
            _context = context;
        }

        public async Task<int> CountAsync()
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Product.CountAsync();
            }
        }

        public async Task CreateAsync(IEnumerable<Product> entities)
        {
            if (entities == null || entities.Count() == 0)
            {
                return;
            }

            _context.Product.AddRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<Product> FindAsync(long id)
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Product.SingleOrDefaultAsync(x => x.Id == id);
            } 
        }

        public async Task<List<Product>> FindAsync(uint pageIndex, uint pageSize)
        {
            if (pageIndex < 1 || pageSize <= 0)
            {
                return new List<Product>();
            }

            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Product.Skip((int)((pageIndex - 1) * pageSize)).Take((int)pageSize).ToListAsync();
            } 
        }

        public async Task<List<Product>> FindAsync(Expression<Func<Product, bool>> expression)
        {
            using (TransactionScope ts = Repository.GetNewReadUncommittedScope())
            {
                return await _context.Product.AsNoTracking().Where(expression).ToListAsync();
            } 
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
