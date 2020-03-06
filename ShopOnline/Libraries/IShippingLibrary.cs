using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Libraries
{
    public interface IShippingLibrary
    {
        Task CreateAsync(IEnumerable<Order> orders);
    }
}
