using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtensionLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Models;
using ShopOnline.Repository;

namespace ShopOnline.Controllers
{
    public class OrderController : Controller
    {
        private readonly IRepository<Order> _orderRepository;

        public OrderController(IRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // GET: Order
        public async Task<IActionResult> Index(int pageIndex = 1, int pageSize = 20)
        {
            List<Order> orderList = await _orderRepository.FindAsync(pageIndex, pageSize);
            int totalCount = await _orderRepository.CountAsync();
            return View(new PaginatedList<Order>(orderList, totalCount, pageIndex, pageSize));
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<long> statusPaid)
        {
            if (statusPaid == null || statusPaid.Count() == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            List<Order> orderList = await _orderRepository.FindAsync(x => statusPaid.Contains(x.Id));
            if (orderList != null && orderList.Count > 0)
            {
                foreach (Order order in orderList)
                {
                    order.Status = OrderStatus.ToBeShipped;
                }
                await _orderRepository.UpdateAsync(orderList);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
