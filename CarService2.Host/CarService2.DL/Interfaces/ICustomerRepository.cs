using CarService3.Models.Entities;

namespace CarService3.DL.Interfaces
{
    public interface ICustomerRepository
    {
        Task Add(Customer? customer);
        Task<List<Customer>> GetAll();
        Task<Customer?> GetById(Guid id);
        Task Delete(Guid id);
    }
}
