using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace TheHunter.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET: Booking
        // Hiển thị danh sách và lọc theo tên khách hoặc tên sân
        public IActionResult Index(string searchTerm)
        {
            var bookingList = _bookingService.GetAll();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                bookingList = bookingList.Where(b =>
                    b.CustomerName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    b.Court.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
                ViewData["CurrentFilter"] = searchTerm;
            }

            // Thống kê trạng thái hiển thị trên View
            ViewBag.TotalCount = bookingList.Count;
            ViewBag.ConfirmedCount = bookingList.Count(b => b.Status == BookingStatus.Confirmed);
            ViewBag.PendingCount = bookingList.Count(b => b.Status == BookingStatus.Pending);
            ViewBag.CancelledCount = bookingList.Count(b => b.Status == BookingStatus.Cancelled);

            return View(bookingList);
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            var model = new Booking
            {
                Date = DateTime.Today,
                StartHour = DateTime.Now.Hour, // Mặc định là giờ hiện tại
                EndHour = DateTime.Now.Hour + 1 // Mặc định thuê 1 tiếng
            };
            return View(model);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Booking booking)
        {
            // Kiểm tra logic: Giờ kết thúc phải lớn hơn giờ bắt đầu
            if (booking.EndHour <= booking.StartHour)
            {
                ModelState.AddModelError("EndHour", "Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            }

            if (ModelState.IsValid)
            {
                _bookingService.Add(booking);
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }
        // GET: Booking/Edit/5
        public IActionResult Edit(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }

        // GET: Booking/Delete/5
        public IActionResult Delete(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null) return NotFound();
            return View(booking);
        }
        // POST: Booking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Booking booking)
        {
            if (id != booking.Id) return BadRequest();

            if (booking.EndHour <= booking.StartHour)
            {
                ModelState.AddModelError("EndHour", "Giờ kết thúc phải lớn hơn giờ bắt đầu.");
            }

            if (ModelState.IsValid)
            {
                _bookingService.Update(booking);
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _bookingService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // API phụ: Thay đổi trạng thái Booking nhanh chóng
        [HttpPost]
        public IActionResult UpdateStatus(int id, BookingStatus status)
        {
            var booking = _bookingService.GetById(id);
            if (booking != null)
            {
                booking.Status = status;
                _bookingService.Update(booking);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}