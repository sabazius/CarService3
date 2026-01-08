using CarService3.BL.Interfaces;
using CarService3.DL.Interfaces;
using CarService3.Models.Responses;

namespace CarService3.BL.Services
{
    internal class SellCarService : ISellCarService
    {
        private readonly ICustomerService _customerService;
        private readonly ICarRepository _carRepository;

        public SellCarService(ICustomerService customerService, ICarRepository carRepository)
        {
            _customerService = customerService;
            _carRepository = carRepository;
        }

        public SellCarResult? SellCar(Guid customerId, Guid carId)
        {
            var customer = _customerService.GetById(customerId);
            var car = _carRepository.GetById(carId);

            if (customer == null || car == null)
            {
                return null;
            }

            var price = car.BasePrice - customer.Discount;

            return new SellCarResult
            {
                Customer = customer,
                Car = car,
                SalePrice = price < 0 ? 0 : price
            };
        }
    }
}
