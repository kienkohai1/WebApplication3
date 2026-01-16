// InventoryController.cs (Updated)
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IProductService _productService;

        public InventoryController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: Inventory
        // Hiển thị danh sách vật tư sân bắn cung, hỗ trợ tìm kiếm theo tên hoặc danh mục
        public IActionResult Index(string searchTerm = null, string category = null)
        {
            var productList = _productService.GetAll();

            ViewBag.Categories = productList.Select(p => p.Category).Distinct().ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                productList = productList.Where(p =>
                    p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    p.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                ViewData["CurrentFilter"] = searchTerm;
            }

            if (!string.IsNullOrEmpty(category) && category != "Tất cả")
            {
                productList = productList.Where(p => p.Category == category).ToList();
            }

            ViewData["CurrentCategory"] = category ?? "Tất cả";

            // Thống kê nhanh để hiển thị trên giao diện
            ViewBag.TotalStockValue = productList.Sum(p => (long)p.StockQuantity * p.ImportPrice);
            ViewBag.TotalRevenue = productList.Sum(p => (long)p.StockQuantity * p.Price);
            ViewBag.TotalStockQuantity = productList.Sum(p => p.StockQuantity);
            ViewBag.LowStockCount = productList.Count(p => p.StockQuantity < 10);

            return View(productList);
        }

        // GET: Inventory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _productService.Add(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Inventory/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Inventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _productService.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Inventory/Delete/5
        public IActionResult Delete(int id)
        {
            var product = _productService.GetById(id);
            if (product == null) return NotFound();
            return View(product);
        }

        // POST: Inventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _productService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Action xử lý nhanh việc nhập thêm kho (UpdateStock)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Restock(int id, int addedQuantity)
        {
            var product = _productService.GetById(id);
            if (product != null && addedQuantity > 0)
            {
                product.StockQuantity += addedQuantity;
                _productService.Update(product);
            }
            return RedirectToAction(nameof(Index));
        }

        // Action xử lý nhanh việc lấy hàng (giảm kho)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deplete(int id, int removedQuantity)
        {
            var product = _productService.GetById(id);
            if (product != null && removedQuantity > 0 && product.StockQuantity >= removedQuantity)
            {
                product.StockQuantity -= removedQuantity;
                _productService.Update(product);
            }
            // Nếu số lượng không đủ, có thể thêm thông báo lỗi qua TempData nếu cần
            return RedirectToAction(nameof(Index));
        }
    }
}