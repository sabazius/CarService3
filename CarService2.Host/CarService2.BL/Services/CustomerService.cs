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

        public void Add(Customer? customer)
        {
            if (customer == null) return;

            customer.Id = Guid.NewGuid();

            _customerRepository.Add(customer);
        }

        public List<Customer> GetAll()
        {
            return _customerRepository.GetAll();
        }

        public Customer? GetById(Guid id)
        {
            return _customerRepository.GetById(id);
        }

        public void Delete(Guid id)
        {
            _customerRepository.Delete(id);
        }
    }
}
