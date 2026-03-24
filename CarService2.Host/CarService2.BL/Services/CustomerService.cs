using CarService3.BL.Interfaces;
using CarService3.DL.Interfaces;
using CarService3.Models.Entities;

namespace CarService3.BL.Services
{
    internal class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(
            ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task Add(Customer? customer)
        {
            if (customer == null) return;

            customer.Id = Guid.NewGuid();

            await _customerRepository.Add(customer);
        }

        public async Task<List<Customer>> GetAll()
        {
           return await _customerRepository.GetAll();
        }

        public async Task<Customer?> GetById(Guid id)
        {
            return await _customerRepository.GetById(id);
        }

        public async Task Delete(Guid id)
        {
            await _customerRepository.Delete(id);
        }
    }
}
