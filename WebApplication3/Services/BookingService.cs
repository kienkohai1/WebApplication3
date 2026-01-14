using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class BookingService : IBookingService
    {
        // Sử dụng static list để dữ liệu không bị mất khi chuyển trang
        private static List<Booking> _bookings = new List<Booking>
        {
            new Booking { Id = 1, CustomerName = "Nguyễn Văn A", Phone = "0901234567", Court = "Sân 1", Date = DateTime.Now, TimeSlot = "08:00 - 10:00", Price = 200000, Status = BookingStatus.Confirmed },
            new Booking { Id = 2, CustomerName = "Trần Thị B", Phone = "0912345678", Court = "Sân 3", Date = DateTime.Now.AddDays(1), TimeSlot = "17:00 - 19:00", Price = 250000, Status = BookingStatus.Pending }
        };

        public List<Booking> GetAll() => _bookings;

        public Booking? GetById(int id) => _bookings.FirstOrDefault(b => b.Id == id);

        public void Add(Booking booking)
        {
            // Tự động tăng ID
            booking.Id = _bookings.Any() ? _bookings.Max(b => b.Id) + 1 : 1;
            _bookings.Add(booking);
        }

        public void Update(Booking booking)
        {
            var existing = GetById(booking.Id);
            if (existing != null)
            {
                existing.CustomerName = booking.CustomerName;
                existing.Phone = booking.Phone;
                existing.Court = booking.Court;
                existing.Date = booking.Date;
                existing.TimeSlot = booking.TimeSlot;
                existing.Price = booking.Price;
                existing.Status = booking.Status;
            }
        }

        public void Delete(int id)
        {
            var booking = GetById(id);
            if (booking != null) _bookings.Remove(booking);
        }
    }
}