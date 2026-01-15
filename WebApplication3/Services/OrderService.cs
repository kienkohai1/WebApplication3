using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IOrderService
    {
        List<ServiceOrder> GetAll();
        void Add(ServiceOrder order);
        void UpdateStatus(int id, OrderStatus status);
        void Delete(int id);
    }

    public class OrderService : IOrderService
    {
        private static List<ServiceOrder> _orders = new List<ServiceOrder>();

        // Bảng giá định sẵn (Bạn có thể thêm bớt ở đây)
        private readonly Dictionary<string, decimal> _priceList = new Dictionary<string, decimal>
        {
            { "Nước suối", 10000 },
            { "Bia tập", 25000 },
            { "Mũi tên", 50000 },
            { "Khăn lạnh", 5000 }
        };

        public List<ServiceOrder> GetAll()
        {
            return _orders
                .OrderBy(o => o.Status == OrderStatus.Waiting ? 0 : 1) // Chờ phục vụ (Waiting) lên đầu
                .ThenByDescending(o => o.Id) // Các đơn cùng trạng thái thì đơn mới nhất ở trên
                .ToList();
        }
        public void Add(ServiceOrder order)
        {
            order.Id = _orders.Any() ? _orders.Max(o => o.Id) + 1 : 1;

            // Logic tính tiền giữ nguyên
            if (_priceList.TryGetValue(order.ItemName, out decimal price))
            {
                order.UnitPrice = price;
            }
            order.TotalPrice = order.UnitPrice * order.Quantity;

            _orders.Add(order);
        }


        public void UpdateStatus(int id, OrderStatus status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null) order.Status = status;
        }

        public void Delete(int id) => _orders.RemoveAll(o => o.Id == id);
    }
}