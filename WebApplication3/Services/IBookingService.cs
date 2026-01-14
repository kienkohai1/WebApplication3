using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IBookingService
    {
        List<Booking> GetAll();
        Booking? GetById(int id);
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(int id);
    }
}