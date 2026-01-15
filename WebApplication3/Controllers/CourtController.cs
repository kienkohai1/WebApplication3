using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    public class CourtController : Controller
    {
        private readonly ICourtService _courtService;

        public CourtController(ICourtService courtService)
        {
            _courtService = courtService;
        }

        // GET: Court
        // Hiển thị danh sách sân, hỗ trợ tìm kiếm và thống kê
        public IActionResult Index(string searchTerm)
        {
            var courtList = _courtService.GetAll();

            // Logic tìm kiếm theo tên sân hoặc loại sân
            if (!string.IsNullOrEmpty(searchTerm))
            {
                courtList = courtList.Where(c =>
                    c.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    c.Type.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                ViewData["CurrentFilter"] = searchTerm;
            }

            // Tính toán thống kê cho các thẻ Badge
            ViewBag.TotalCount = courtList.Count;
            ViewBag.AvailableCount = courtList.Count(c => c.Status == CourtStatus.Available);
            ViewBag.OccupiedCount = courtList.Count(c => c.Status == CourtStatus.Occupied);
            ViewBag.MaintenanceCount = courtList.Count(c => c.Status == CourtStatus.Maintenance);

            return View(courtList);
        }

        // GET: Court/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Court/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Court court)
        {
            if (ModelState.IsValid)
            {
                _courtService.Add(court);
                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Court/Edit/5
        public IActionResult Edit(int id)
        {
            var court = _courtService.GetById(id);
            if (court == null) return NotFound();
            return View(court);
        }

        // POST: Court/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Court court)
        {
            if (id != court.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                _courtService.Update(court);
                return RedirectToAction(nameof(Index));
            }
            return View(court);
        }

        // GET: Court/Delete/5
        public IActionResult Delete(int id)
        {
            var court = _courtService.GetById(id);
            if (court == null) return NotFound();
            return View(court);
        }

        // POST: Court/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _courtService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Action xử lý nhanh việc nhận sân (Check-in)
        [HttpPost]
        public IActionResult CheckIn(int id, string customerName, int hours)
        {
            if (!string.IsNullOrEmpty(customerName))
            {
                _courtService.UpdateOccupancy(id, customerName, hours, true);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult CheckOut(int id)
        {
            _courtService.UpdateOccupancy(id, null, 0, false);

            return RedirectToAction(nameof(Index));
        }


        // API phụ: Thay đổi trạng thái bảo trì nhanh
        [HttpPost]
        public IActionResult ToggleMaintenance(int id)
        {
            var court = _courtService.GetById(id);
            if (court != null)
            {
                court.Status = (court.Status == CourtStatus.Maintenance)
                                ? CourtStatus.Available
                                : CourtStatus.Maintenance;
                _courtService.Update(court);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}