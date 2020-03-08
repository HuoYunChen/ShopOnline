using ShopOnline.Models;
using ShopOnline.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopOnline.Libraries
{
    public class ShippingLibrary : IShippingLibrary
    {
        private IRepository<Shipping> _shippingrderRepository;

        public ShippingLibrary(IRepository<Shipping> shippingrderRepository)
        {
            _shippingrderRepository = shippingrderRepository;
        }

        public Task CreateAsync(IEnumerable<Order> orders)
        {
            if (orders == null)
            {
                return null;
            }

            List<Shipping> shippingList = new List<Shipping>(orders.Count());
            foreach (Order order in orders)
            {
                order.Status = OrderStatus.ToBeShipped; 
                shippingList.Add(new Shipping
                {
                    Price = order.Product.Price,
                    Order = order
                });
            }

            return _shippingrderRepository.UpdateAsync(shippingList);
        }

    }
}
