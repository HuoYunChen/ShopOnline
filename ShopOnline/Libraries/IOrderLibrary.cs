using ExtensionLibrary;
using ShopOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Libraries
{
    public interface IOrderLibrary
    {
        Task StatusChangeAsync(OrderStatus newStatus, IEnumerable<long> ids);

        Task<PaginatedList<Order>> GetListAsync(uint pageIndex, uint pageSize);
    }
}
