using CarService3.Models.Entities;

namespace CarService3.BL.Interfaces
{
    public interface ICarService
    {
        void Add(Car? customer);
        List<Car> GetAll();
        Car? GetById(Guid id);
        void Delete(Guid id);
    }
}
