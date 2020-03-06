using ShopOnline.Models;
using ShopOnline.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Libraries
{
    public class ProductLibrary : IProductLibrary
    {
        private IRepository<Product> _productRepository;

        public ProductLibrary(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<Product> Get(long id)
        {
            return _productRepository.FindAsync(id);
        }
    }
}
