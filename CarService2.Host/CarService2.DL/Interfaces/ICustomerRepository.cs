using CarService3.Models.Entities;

namespace CarService3.DL.Interfaces
{
    public interface ICustomerRepository
    {
        void Add(Customer? customer);
        List<Customer> GetAll();
        Customer? GetById(Guid id);
        void Delete(Guid id);
    }
}
