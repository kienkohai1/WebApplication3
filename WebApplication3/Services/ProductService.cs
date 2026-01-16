using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class ProductService : IProductService
    {
        // Khởi tạo danh sách tĩnh với giá nhập giả lập
        private static List<Product> _products = new List<Product>
        {
            new Product {
                Id = 1, Name = "Bia giấy 60cm", StockQuantity = 200,
                ImportPrice = 10000, Price = 15000,
                Category = "Vật tư", Description = "Bia tiêu chuẩn"
            },
            new Product {
                Id = 2, Name = "Mũi tên tập luyện", StockQuantity = 50,
                ImportPrice = 55000, Price = 80000,
                Category = "Phụ kiện", Description = "Sợi thủy tinh"
            }
        };

        public List<Product> GetAll() => _products;

        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        public void Add(Product product)
        {
            // Tự động tăng Id giống logic trong BookingService
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existing = GetById(product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Description = product.Description;
                existing.StockQuantity = product.StockQuantity;
                existing.ImportPrice = product.ImportPrice; // Cập nhật giá nhập
                existing.Price = product.Price;
                existing.Category = product.Category;
            }
        }

        public void Delete(int id) => _products.RemoveAll(p => p.Id == id);
    }
}