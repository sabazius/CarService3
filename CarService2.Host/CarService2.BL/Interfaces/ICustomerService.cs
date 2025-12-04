using CarService3.Models.Entities;

namespace CarService3.BL.Interfaces
{
    public interface ICustomerService
    {
        void Add(Customer? customer);
        List<Customer> GetAll();
        Customer? GetById(Guid id);
        void Delete(Guid id);
    }
}
