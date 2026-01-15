using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class CourtService : ICourtService
    {
        // Dữ liệu giả lập lưu trong bộ nhớ (Singleton)
        private static List<Court> _courtList = new List<Court>
        {
            new Court {
                Id = 1, Name = "Sân 01", Type = "Sân VIP",
                Status = CourtStatus.Occupied,
                CurrentCustomerName = "Nguyễn Văn A",
                OccupiedSince = DateTime.Now.AddMinutes(-30),
                Description = "Sân có mái che"
            },
            new Court { Id = 2, Name = "Sân 02", Type = "Sân Thường", Status = CourtStatus.Available, Description = "Sân ngoài trời" },
            new Court { Id = 3, Name = "Sân 03", Type = "Sân Thường", Status = CourtStatus.Maintenance, Description = "Bảo trì lưới" }
        };

        public List<Court> GetAll() => _courtList;

        public Court? GetById(int id) => _courtList.FirstOrDefault(c => c.Id == id);

        public void Add(Court court)
        {
            court.Id = _courtList.Any() ? _courtList.Max(c => c.Id) + 1 : 1;
            _courtList.Add(court);
        }

        public void Update(Court court)
        {
            var index = _courtList.FindIndex(c => c.Id == court.Id);
            if (index != -1)
            {
                // Nếu trạng thái không phải Đang sử dụng, tự động xóa thông tin khách hàng
                if (court.Status != CourtStatus.Occupied)
                {
                    court.CurrentCustomerName = null;
                    court.OccupiedSince = null;
                }
                _courtList[index] = court;
            }
        }

        public void Delete(int id) => _courtList.RemoveAll(c => c.Id == id);

        public void UpdateOccupancy(int courtId, string? customerName, int hours, bool isOccupied)
        {
            var court = GetById(courtId);
            if (court != null)
            {
                if (isOccupied) // Check-in (Nhận sân)
                {
                    court.Status = CourtStatus.Occupied;
                    court.CurrentCustomerName = customerName;
                    court.OccupiedSince = DateTime.Now;
                    court.OccupiedUntil = DateTime.Now.AddHours(hours); // Thêm dòng này để tính thời gian kết thúc
                }
                else // Check-out (Trả sân)
                {
                    court.Status = CourtStatus.Available;
                    court.CurrentCustomerName = null;
                    court.OccupiedSince = null;
                    court.OccupiedUntil = null; // Thêm dòng này để xóa thời gian kết thúc
                }
            }
        }


    }
}