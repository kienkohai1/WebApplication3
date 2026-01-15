using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class BookingService : IBookingService
    {
        private static List<Booking> _bookings = new List<Booking>();

        public List<Booking> GetAll() => _bookings;

        public Booking? GetById(int id) => _bookings.FirstOrDefault(b => b.Id == id);

        public void Add(Booking booking)
        {
            booking.Id = _bookings.Any() ? _bookings.Max(b => b.Id) + 1 : 1;
            CalculateTotalPrice(booking);
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
                existing.StartHour = booking.StartHour;
                existing.EndHour = booking.EndHour;
                existing.TicketType = booking.TicketType;
                existing.Status = booking.Status;

                CalculateTotalPrice(existing);
            }
        }

        public void Delete(int id) => _bookings.RemoveAll(b => b.Id == id);

        private void CalculateTotalPrice(Booking booking)
        {
            decimal hourlyRate = booking.TicketType switch
            {
                TicketType.VIP => 300000,
                TicketType.Student => 100000,
                _ => 150000
            };

            // Tính số giờ bằng cách lấy giờ kết thúc trừ giờ bắt đầu
            int totalHours = booking.EndHour - booking.StartHour;

            booking.TotalPrice = totalHours > 0 ? (decimal)totalHours * hourlyRate : 0;
        }
    }
}