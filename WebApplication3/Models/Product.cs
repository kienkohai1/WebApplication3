using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên sản phẩm")]
        [Display(Name = "Tên sản phẩm")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Mô tả")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng nhập số lượng tồn")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng không được âm")]
        [Display(Name = "Số lượng tồn")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá nhập")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá nhập không hợp lệ")]
        [Display(Name = "Giá nhập (VNĐ)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal ImportPrice { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập giá bán")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán không hợp lệ")]
        [Display(Name = "Giá bán (VNĐ)")]
        [DisplayFormat(DataFormatString = "{0:N0}")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        [Display(Name = "Danh mục")]
        public string Category { get; set; } = "Vật tư bắn cung";
    }
}