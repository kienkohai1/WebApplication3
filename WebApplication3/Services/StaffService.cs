using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Services
{
    public class StaffService : IStaffService
    {
        private List<Staff> _staffList;

        public StaffService()
        {
            // Khởi tạo dữ liệu mẫu giống StaffManagement.tsx
            _staffList = new List<Staff>
            {
                new Staff { Id = 1, Name = "Nguyễn Văn An", Position = "Quản lý", Phone = "0901234567", Email = "an.nguyen@example.com", Status = StaffStatus.Active, Shift = "Ca sáng" },
                new Staff { Id = 2, Name = "Trần Thị Bình", Position = "Lễ tân", Phone = "0902345678", Email = "binh.tran@example.com", Status = StaffStatus.Active, Shift = "Ca sáng" },
                new Staff { Id = 3, Name = "Lê Văn Cường", Position = "Bảo vệ", Phone = "0903456789", Email = "cuong.le@example.com", Status = StaffStatus.Active, Shift = "Ca đêm" },
                new Staff { Id = 4, Name = "Phạm Thị Dung", Position = "Huấn luyện viên", Phone = "0904567890", Email = "dung.pham@example.com", Status = StaffStatus.Active, Shift = "Ca chiều" },
                new Staff { Id = 5, Name = "Hoàng Văn Em", Position = "Lễ tân", Phone = "0905678901", Email = "em.hoang@example.com", Status = StaffStatus.Inactive, Shift = "Ca sáng" }
            };
        }

        public List<Staff> GetAll() => _staffList;

        public Staff? GetById(int id) => _staffList.FirstOrDefault(s => s.Id == id);

        public void Add(Staff staff)
        {
            staff.Id = _staffList.Any() ? _staffList.Max(s => s.Id) + 1 : 1;
            _staffList.Add(staff);
        }

        public void Update(Staff staff)
        {
            var index = _staffList.FindIndex(s => s.Id == staff.Id);
            if (index != -1) _staffList[index] = staff;
        }

        public void Delete(int id) => _staffList.RemoveAll(s => s.Id == id);
    }
}