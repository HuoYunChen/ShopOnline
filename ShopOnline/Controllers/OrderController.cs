using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtensionLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ShopOnline.Libraries;
using ShopOnline.Logger;
using ShopOnline.Models;

namespace ShopOnline.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderLibrary _OrderLibrary;

        public OrderController(ILogger<OrderController> logger, IOrderLibrary orderLibrary)
        {
            _logger = logger;
            _OrderLibrary = orderLibrary;
        }

        public async Task<IActionResult> Index(uint pageIndex = 1, uint pageSize = 20)
        {
            PaginatedList<Order> orderList = await _OrderLibrary.GetListAsync(pageIndex, pageSize);
            return View(orderList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(List<long> statusPaid)
        {
            if (statusPaid == null || statusPaid.Count() == 0)
            {
                return RedirectToAction(nameof(Index));
            }

            await _OrderLibrary.StatusChangeAsync(OrderStatus.ToBeShipped, statusPaid);

            var logger = new ControllerLogger<List<long>>(this.ControllerContext, statusPaid);
            _logger.LogTrace(logger.GetMessage());

            return RedirectToAction(nameof(Index));
        }
    }
}
