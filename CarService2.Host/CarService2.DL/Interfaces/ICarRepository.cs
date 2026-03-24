using CarService3.Models.Entities;

namespace CarService3.DL.Interfaces
{
    public interface ICarRepository
    {
        Task Add(Car? customer);
        List<Car> GetAll();
        Task<Car?> GetById(Guid id);
        void Delete(Guid id);
    }
}
