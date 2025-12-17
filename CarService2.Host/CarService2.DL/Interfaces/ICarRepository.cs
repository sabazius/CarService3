using CarService3.Models.Entities;

namespace CarService3.DL.Interfaces
{
    public interface ICarRepository
    {
        void Add(Car? customer);
        List<Car> GetAll();
        Car? GetById(Guid id);
        void Delete(Guid id);
    }
}
