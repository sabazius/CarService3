using CarService3.DL.Interfaces;
using CarService3.Models.Entities;

namespace CarService3.DL.Repositories
{
    internal class CarStaticRepository : ICarRepository
    {
        public void Add(Car? car)
        {
            if (car == null) return;

            MyStaticDb.StaticDb.Cars.Add(car);
        }

        public List<Car> GetAll()
        {
            return MyStaticDb.StaticDb.Cars;
        }

        public Car? GetById(Guid id)
        {
            if (id != Guid.Empty) return null;

            return MyStaticDb.StaticDb
                .Cars
                .FirstOrDefault(c => c.Id == id);
        }

        public void Delete(Guid id)
        {
            if (id != Guid.Empty) return;

            var car = GetById(id);

            if (car != null)
            {
                MyStaticDb.StaticDb.Cars.Remove(car);
            }
        }
    }
}
