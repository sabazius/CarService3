using CarService3.Models.Entities;

namespace CarService3.DL.Interfaces
{
    public interface ICarRepository
    {
        void Add(Car? car);
        List<Car> GetAll();
        Car? GetById(Guid id);
        void Delete(Guid id);
    }
}
