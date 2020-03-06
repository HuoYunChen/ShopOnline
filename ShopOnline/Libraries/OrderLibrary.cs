using ShopOnline.Models;
using ShopOnline.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtensionLibrary;

namespace ShopOnline.Libraries
{
    public class OrderLibrary : IOrderLibrary
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IShippingLibrary _shippingLibrary;

        public OrderLibrary(IRepository<Order> orderRepository, IShippingLibrary shippingLibrary)
        {
            _orderRepository = orderRepository;
            _shippingLibrary = shippingLibrary;
        }

        public async Task StatusChangeAsync (OrderStatus newStatus, IEnumerable<long> ids)
        {
            if (ids == null)
            {
                return;
            }

            switch (newStatus)
            {
                case OrderStatus.ToBeShipped:                    
                {                        
                        List<Order> orderList = await _orderRepository.FindAsync(x => ids.Contains(x.Id));
                        await _shippingLibrary.CreateAsync(orderList);
                }
                break;
            }
        }

        private async Task UpdateStatus(OrderStatus newStatus, IEnumerable<Order> orders)
        {
            foreach (Order order in orders)
            {
                order.Status = OrderStatus.ToBeShipped;
            }

            await _orderRepository.UpdateAsync(orders);
        }

        public async Task<PaginatedList<Order>> GetListAsync (uint pageIndex = 1, uint pageSize = 20)
        {
            List<Order> orderList = await _orderRepository.FindAsync(pageIndex, pageSize);
            int totalCount = await _orderRepository.CountAsync();
            return new PaginatedList<Order>(orderList, totalCount, pageIndex, pageSize);
        }
    }
}
