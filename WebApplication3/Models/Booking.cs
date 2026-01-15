using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        [Display(Name = "Tên khách hàng")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn sân")]
        [Display(Name = "Sân")]
        public string Court { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Vui lòng chọn giờ bắt đầu")]
        [Range(0, 23, ErrorMessage = "Giờ phải từ 0 đến 23")]
        public int StartHour { get; set; } // Đổi từ StartTime

        [Required(ErrorMessage = "Vui lòng chọn giờ kết thúc")]
        [Range(0, 23, ErrorMessage = "Giờ phải từ 0 đến 23")]
        public int EndHour { get; set; }

        [Required]
        [Display(Name = "Loại vé")]
        public TicketType TicketType { get; set; }

        [Display(Name = "Tổng tiền (VNĐ)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Trạng thái")]
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
    }

    public enum TicketType
    {
        [Display(Name = "Thường (150k/h)")]
        Standard,
        [Display(Name = "VIP (300k/h)")]
        VIP,
        [Display(Name = "Sinh viên (100k/h)")]
        Student
    }

    public enum BookingStatus
    {
        [Display(Name = "Đã xác nhận")]
        Confirmed,
        [Display(Name = "Chờ duyệt")]
        Pending,
        [Display(Name = "Đã hủy")]
        Cancelled
    }
}