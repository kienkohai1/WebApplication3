// Services/IStaffService.cs
using WebApplication3.Models;
namespace WebApplication3.Services
{
    public interface IStaffService
    {
        List<Staff> GetAll();
        Staff? GetById(int id);
        void Add(Staff staff);
        void Update(Staff staff);
        void Delete(int id);
    }
}