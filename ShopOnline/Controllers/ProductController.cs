using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopOnline.Data;
using ShopOnline.Libraries;
using ShopOnline.Models;
using ShopOnline.Repository;

namespace ShopOnline.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductLibrary _productLibrary;

        public ProductController(IProductLibrary productLibrary)
        {
            _productLibrary = productLibrary;
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productLibrary.Get((long)id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

    }
}
