using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Court
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sân")]
        [Display(Name = "Tên sân")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn loại sân")]
        [Display(Name = "Loại sân")]
        public string Type { get; set; } = string.Empty;

        [Display(Name = "Trạng thái")]
        public CourtStatus Status { get; set; } = CourtStatus.Available;

        // Các thuộc tính bổ sung cho trường hợp Đang sử dụng
        [Display(Name = "Người đang sử dụng")]
        public string? CurrentCustomerName { get; set; }

        [Display(Name = "Bắt đầu lúc")]
        [DisplayFormat(DataFormatString = "{0:HH:mm dd/MM}")]
        public DateTime? OccupiedUntil { get; set; }
        public DateTime? OccupiedSince { get; set; }

        [Display(Name = "Mô tả")]
        public string Description { get; set; } = string.Empty;
    }

    public enum CourtStatus
    {
        [Display(Name = "Sẵn sàng")]
        Available,
        [Display(Name = "Đang bảo trì")]
        Maintenance,
        [Display(Name = "Đang sử dụng")]
        Occupied
    }
}