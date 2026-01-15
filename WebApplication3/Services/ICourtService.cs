using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface ICourtService
    {
        List<Court> GetAll();

        Court? GetById(int id);

        void Add(Court court);

        void Update(Court court);

        void Delete(int id);
        void UpdateOccupancy(int courtId, string? customerName, int hours, bool isOccupied);
    }
}