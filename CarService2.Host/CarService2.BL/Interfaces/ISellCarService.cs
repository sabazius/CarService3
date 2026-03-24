using CarService3.Models.Responses;

namespace CarService3.BL.Interfaces
{
    public interface ISellCarService
    {
        Task<SellCarResult?> SellCar(Guid customerId, Guid carId);
    }
}
