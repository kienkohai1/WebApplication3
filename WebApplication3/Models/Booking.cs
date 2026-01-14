namespace WebApplication3.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Court { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string TimeSlot { get; set; } = string.Empty; // Ví dụ: "10:00 - 12:00"
        public decimal Price { get; set; }
        public BookingStatus Status { get; set; }
    }

    public enum BookingStatus
    {
        Confirmed, // Đã xác nhận
        Pending,   // Chờ xác nhận
        Cancelled  // Đã hủy
    }
}
