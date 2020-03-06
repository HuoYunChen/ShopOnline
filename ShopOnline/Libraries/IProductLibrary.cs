using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Libraries
{
    public interface IProductLibrary
    {
        Task<Product> Get(long id);
    }
}
