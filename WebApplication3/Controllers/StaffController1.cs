using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplication3.Models;
using WebApplication3.Services;

namespace TheHunter.Controllers
{
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        // Dependency Injection thông qua Constructor
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        // GET: Staff
        // Hiển thị danh sách và xử lý tìm kiếm (giống thanh search trong React)
        public IActionResult Index(string searchTerm)
        {
            var staffList = _staffService.GetAll();

            // Logic tìm kiếm giống như staffList.filter(...) trong React
            if (!string.IsNullOrEmpty(searchTerm))
            {
                staffList = staffList.Where(s =>
                    s.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    s.Position.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                ViewData["CurrentFilter"] = searchTerm;
            }

            // Tính toán thống kê cho các thẻ Badge ở đầu trang
            ViewBag.TotalCount = staffList.Count;
            ViewBag.ActiveCount = staffList.Count(s => s.Status == StaffStatus.Active);
            ViewBag.InactiveCount = staffList.Count(s => s.Status == StaffStatus.Inactive);

            return View(staffList);
        }

        // GET: Staff/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Staff staff)
        {
            if (ModelState.IsValid)
            {
                _staffService.Add(staff);
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staff/Edit/5
        public IActionResult Edit(int id)
        {
            var staff = _staffService.GetById(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Staff staff)
        {
            if (id != staff.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _staffService.Update(staff);
                return RedirectToAction(nameof(Index));
            }
            return View(staff);
        }

        // GET: Staff/Delete/5 (Trang xác nhận xóa)
        public IActionResult Delete(int id)
        {
            var staff = _staffService.GetById(id);
            if (staff == null)
            {
                return NotFound();
            }
            return View(staff);
        }

        // POST: Staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _staffService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // API phụ: Thay đổi trạng thái nhanh (Dùng cho nút chuyển đổi Active/Inactive)
        [HttpPost]
        public IActionResult ToggleStatus(int id)
        {
            var staff = _staffService.GetById(id);
            if (staff != null)
            {
                staff.Status = (staff.Status == StaffStatus.Active) ? StaffStatus.Inactive : StaffStatus.Active;
                _staffService.Update(staff);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}