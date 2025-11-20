using CarService3.Models.Entities;

namespace CarService3.BL.Interfaces
{
    public interface ICustomerService
    {
        void Add(Customer? customer);
        List<Customer> GetAll();
        Customer? GetById(int id);
        void Delete(int id);
    }
}
