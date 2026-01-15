using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class ServiceOrder
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách")]
        [Display(Name = "Khách hàng")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn sân")]
        [Display(Name = "Vị trí sân")]
        public string CourtName { get; set; } = string.Empty; // Sân 1 -> Sân 8

        [Required(ErrorMessage = "Chọn món")]
        [Display(Name = "Dịch vụ/Sản phẩm")]
        public string ItemName { get; set; } = string.Empty;

        [Range(1, 100, ErrorMessage = "Số lượng từ 1-100")]
        [Display(Name = "Số lượng")]
        public int Quantity { get; set; } = 1;
        [Display(Name = "Đơn giá")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Tổng cộng")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Trạng thái")]
        public OrderStatus Status { get; set; } = OrderStatus.Waiting;
    }

    public enum OrderStatus
    {
        [Display(Name = "Chờ phục vụ")]
        Waiting,
        [Display(Name = "Đang phục vụ")]
        InService,
        [Display(Name = "Hoàn thành")]
        Completed,
        [Display(Name = "Từ chối")]
        Rejected
    }
}