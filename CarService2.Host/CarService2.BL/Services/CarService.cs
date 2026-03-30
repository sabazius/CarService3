using CarService3.BL.Interfaces;
using CarService3.DL.Interfaces;
using CarService3.Models.Entities;

namespace CarService3.BL.Services
{
    internal class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(
            ICarRepository CarRepository)
        {
            _carRepository = CarRepository;
        }

        public void Add(Car? Car)
        {
            if (Car == null) return;

            Car.Id = Guid.NewGuid();

            _carRepository.Add(Car);
        }

        public List<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public Car? GetById(Guid id)
        {
            return _carRepository.GetById(id);
        }

        public void Delete(Guid id)
        {
            _carRepository.Delete(id);
        }
    }
}
