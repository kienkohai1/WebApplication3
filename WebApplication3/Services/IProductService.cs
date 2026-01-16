using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IProductService
    {
        List<Product> GetAll();
        Product? GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }
}